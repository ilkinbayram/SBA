using Core.DataAccess;
using Core.Entities.Concrete.System;

namespace SBA.DataAccess.Abstract
{
    public interface IBundleDal : IEntityRepository<Bundle>, IEntityQueryableRepository<Bundle>
    {
    }
}
