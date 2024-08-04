using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface IPredictionDal : IEntityRepository<Prediction>, IEntityQueryableRepository<Prediction>
    {
    }
}
