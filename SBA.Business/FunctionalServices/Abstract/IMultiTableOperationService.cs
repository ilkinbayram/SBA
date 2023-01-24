using Core.Utilities.Results;

namespace SBA.Business.FunctionalServices.Abstract
{
    public interface IMultiTableOperationService
    {
        IDataResult<int> InitializeNewColumnsOnFilterResult();
        IDataResult<int> DeleteNonMatchedDataes();
    }
}
