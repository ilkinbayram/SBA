using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using SBA.DataAccess.Abstract;
using SBA.ExternalDataAccess.Concrete.EntityFramework.Contexts;

namespace SBA.ExternalDataAccess.Concrete
{
    public class EfExtLogDal : EfEntityRepositoryBase<Log, ExternalAppDbContext>, IExtLogDal
    {
        public EfExtLogDal(ExternalAppDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
