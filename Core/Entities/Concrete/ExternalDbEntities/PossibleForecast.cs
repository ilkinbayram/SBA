namespace Core.Entities.Concrete.ExternalDbEntities
{
    public class PossibleForecast : IEntity
    {
        public int Id { get; set; }
        public int Serial { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdateVersion { get; set; }
    }
}
