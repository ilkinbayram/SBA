using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface IBetSystemDal : IEntityRepository<BetSystem>, IEntityQueryableRepository<BetSystem>
    {
    }
}
