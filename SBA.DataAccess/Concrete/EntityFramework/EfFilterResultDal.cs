using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfFilterResultDal : EfEntityRepositoryBase<FilterResult, ApplicationDbContext>, IFilterResultDal
    {
        public EfFilterResultDal(ApplicationDbContext context) : base(context)
        {

        }
    }
}
