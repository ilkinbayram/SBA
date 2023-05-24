using Core.Entities.Concrete.Base;

namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class StatisticInfoHolder : Identifier, IEntity
    {
        public StatisticInfoHolder(Guid parentId,
                                   int order,
                                   string title,
                                   string homeValue,
                                   string awayValue,
                                   decimal homePercent,
                                   decimal awayPercent,
                                   int serial,
                                   int statisticType,
                                   int bySideType)
        {
            ParentId = parentId;
            Order = order;
            Title = title;
            HomeValue = homeValue;
            AwayValue = awayValue;
            HomePercent = homePercent;
            AwayPercent = awayPercent;
            Serial = serial;
            StatisticType = statisticType;
            BySideType = bySideType;

        }

        public StatisticInfoHolder()
        {
            
        }

        public Guid ParentId { get; set; }
        public int Order { get; set; }
        public int Serial { get; set; }
        public int StatisticType { get; set; }
        public int BySideType { get; set; }
        public string Title { get; set; }
        public string HomeValue { get; set; }
        public string AwayValue { get; set; }
        public decimal HomePercent { get; set; }
        public decimal AwayPercent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
