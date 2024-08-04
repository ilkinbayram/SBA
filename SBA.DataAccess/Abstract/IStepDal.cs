using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface IStepDal : IEntityRepository<Step>, IEntityQueryableRepository<Step>
    {
    }
}
