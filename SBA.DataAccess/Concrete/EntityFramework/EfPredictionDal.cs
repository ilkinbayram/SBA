using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete.System;
using DataAccess.Concrete.EntityFramework.Contexts;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.EntityFramework
{
    public class EfPredictionDal : EfEntityRepositoryBase<Prediction, ApplicationDbContext>, IPredictionDal
    {
        public EfPredictionDal(ApplicationDbContext applicationContext) : base(applicationContext)
        {
        }
    }
}
