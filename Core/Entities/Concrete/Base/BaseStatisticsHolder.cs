namespace Core.Entities.Concrete.Base
{
    public class BaseStatisticsHolder : Identifier
    {
        public int MatchIdentifierId { get; set; }

        public Guid UniqueIdentity { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
