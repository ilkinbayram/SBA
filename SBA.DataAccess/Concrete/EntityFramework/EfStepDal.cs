using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfStepDal : EfEntityRepositoryBase<Step, ApplicationDbContext>, IStepDal
    {
        public EfStepDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
