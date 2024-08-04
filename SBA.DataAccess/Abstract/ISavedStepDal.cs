using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface ISavedStepDal : IEntityRepository<SavedStep>, IEntityQueryableRepository<SavedStep>
    {
    }
}
