using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface IComboBetDal : IEntityRepository<ComboBet>, IEntityQueryableRepository<ComboBet>
    {
    }
}
