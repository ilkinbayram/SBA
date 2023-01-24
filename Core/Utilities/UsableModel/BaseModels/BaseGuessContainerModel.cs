namespace Core.Utilities.UsableModel.BaseModels
{
    public class BaseGuessContainerModel
    {
        public string Serial { get; set; }
        public string CountryName { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }

        public GuessModel HomeAway { get; set; }
        public GuessModel General { get; set; }
    }
}
