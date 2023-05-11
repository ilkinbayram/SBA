using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.ExternalDbEntities;
using SBA.ExternalDataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfAiDataHolderDal : EfEntityRepositoryBase<AiDataHolder, ExternalAppDbContext>, IAiDataHolderDal
    {
        public EfAiDataHolderDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
