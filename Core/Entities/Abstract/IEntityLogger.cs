namespace Core.Entities.Abstract
{
    public interface IEntityLogger
    {
        DateTime CreatedDateTime { get; set; }
        DateTime ModifiedDateTime { get; set; }
        string? CreatedBy { get; set; }
        string? ModifiedBy { get; set; }
    }
}
