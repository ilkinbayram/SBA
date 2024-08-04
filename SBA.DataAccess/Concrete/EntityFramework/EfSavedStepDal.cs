using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfSavedStepDal : EfEntityRepositoryBase<SavedStep, ApplicationDbContext>, ISavedStepDal
    {
        public EfSavedStepDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
