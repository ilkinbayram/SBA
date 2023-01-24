using Core.Utilities.Results;
using Core.Utilities.UsableModel.TempTableModels.Country;
using System.Threading.Tasks;

namespace SBA.Business.FunctionalServices.Abstract
{
    public interface IDataMaintenanceService
    {
        IDataResult<int> InitializeNewColumnsOnFilterResult();
        IDataResult<int> DeleteNonMatchedDataes();

        Task<IDataResult<int>> UpdateEmptyCountriesAsync(CountryContainerTemp containertTemp);
    }
}
