using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfLogDal : EfEntityRepositoryBase<Log, ApplicationDbContext>, ILogDal
    {
        public EfLogDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
