using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class ComparisonGuessContainer : BaseGuessContainerModel
    {
        public ComparisonGuessContainer()
        {
            Serial = string.Empty;
            Home = string.Empty;
            Away = string.Empty;

            HomeAway = new GuessModel();
            General = new GuessModel();
        }
    }
}
