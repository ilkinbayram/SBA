using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfBundleDal : EfEntityRepositoryBase<Bundle, ApplicationDbContext>, IBundleDal
    {
        public EfBundleDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
