using Application.Common.Repositories;
using DataAccess.Entities;
using Domain.Common.Filters;
using Infrastructure.Common.Verification;
using Microsoft.Extensions.Options;
using NLog;
using Quartz;
using Sender.Domain.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace Sender.Infrastructure.Quartz.Jobs.Posting;

/// <summary>
/// A job responsible for posting new posts to Telegram channel.
/// </summary>
public class PostToTelegramJob : IJob
{
    private readonly IMediaRepository _mediaRepository;
    private readonly TelegramOptions _telegramOptions;
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Instantiates <see cref="PostToTelegramJob"/>.
    /// </summary>
    /// <param name="telegramBotClient">Telegram bot client.</param>
    /// <param name="mediaRepository">Media repository.</param>
    /// <param name="telegramOptions">Telegram options.</param>
    public PostToTelegramJob(
        ITelegramBotClient telegramBotClient,
        IMediaRepository mediaRepository,
        IOptions<TelegramOptions> telegramOptions)
    {
        _telegramBotClient = telegramBotClient;
        _mediaRepository = mediaRepository;
        _telegramOptions = telegramOptions.Value;
    }

    /// <inheritdoc/>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var medias = await GetMediasAsync();

            if (medias.Count() == 1)
            {
                var onlinePhoto = new InputOnlineFile(medias.Single().Url);

                await _telegramBotClient.SendPhotoAsync(
                    chatId: _telegramOptions.ChatId,
                    photo: onlinePhoto,
                    parseMode: ParseMode.MarkdownV2);
            }
            else
            {
                var albumInputMediaCollection = GetAlbumInputMediaCollection(medias);
                await _telegramBotClient.SendMediaGroupAsync(
                    chatId: _telegramOptions.ChatId,
                    media: albumInputMediaCollection);
            }

            var mediaIds = medias.Select(m => m.Id);
            await _mediaRepository.DeleteManyAsync(mediaIds);
        }
        catch (Exception e)
        {
            _logger.Error(e, $"Error while executing {nameof(PostToTelegramJob)}.");
        }
    }

    private async Task<IEnumerable<Media>> GetMediasAsync()
    {
        var paginationFilter = new PaginationFilter
        {
            PageSize = 1
        };

        var medias = await _mediaRepository.FindAsync(_ => true, paginationFilter);
        var media = medias.SingleOrDefault();
        Verify.EntityFound(media, nameof(media));

        if (!media.GroupKey.HasValue)
        {
            return new[] { media };
        }

        paginationFilter.PageSize = 5;
        medias = await _mediaRepository.FindAsync(m => m.GroupKey == media.GroupKey, paginationFilter);

        return medias;
    }

    private static IEnumerable<IAlbumInputMedia> GetAlbumInputMediaCollection(IEnumerable<Media> medias)
        => medias
            .Select(media => new InputMediaPhoto(new InputMedia(media.Url)))
            .Cast<IAlbumInputMedia>()
            .ToList();
}