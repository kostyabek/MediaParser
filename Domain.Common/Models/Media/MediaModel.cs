using Domain.Common.Models.Base;

namespace Domain.Common.Models.Media;

public class MediaModel : UrlModel
{
    public DateTime CreatedDate { get; set; }
    public Guid? GroupKey { get; set; }
}