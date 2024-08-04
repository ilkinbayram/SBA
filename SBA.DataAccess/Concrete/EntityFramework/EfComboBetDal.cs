using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfComboBetDal : EfEntityRepositoryBase<ComboBet, ApplicationDbContext>, IComboBetDal
    {
        public EfComboBetDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
