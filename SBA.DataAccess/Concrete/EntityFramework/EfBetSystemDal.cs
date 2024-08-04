using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfBetSystemDal : EfEntityRepositoryBase<BetSystem, ApplicationDbContext>, IBetSystemDal
    {
        public EfBetSystemDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
