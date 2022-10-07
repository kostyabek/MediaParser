using DataAccess.Entities.Base;

namespace DataAccess.Entities;

public class Media : BaseIdEntity
{
    public string Url { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? GroupKey { get; set; }
}