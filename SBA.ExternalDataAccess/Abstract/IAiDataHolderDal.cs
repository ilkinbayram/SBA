using Core.DataAccess;
using Core.Entities.Concrete.ExternalDbEntities;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IAiDataHolderDal : IEntityRepository<AiDataHolder>, IEntityQueryableRepository<AiDataHolder>
    {
    }
}
