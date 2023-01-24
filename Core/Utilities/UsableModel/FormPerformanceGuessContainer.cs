using Core.Utilities.UsableModel.BaseModels;

namespace Core.Utilities.UsableModel
{
    public class FormPerformanceGuessContainer : BaseGuessContainerModel
    {
        public FormPerformanceGuessContainer()
        {
            Serial = string.Empty;
            Home = string.Empty;
            Away = string.Empty;

            HomeAway = new GuessModel();
            General = new GuessModel();
        }
    }
}
