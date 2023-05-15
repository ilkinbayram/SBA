using Core.DataAccess;
using Core.Entities.Concrete;

namespace SBA.DataAccess.Abstract
{
    public interface ILogDal : IEntityRepository<Log>, IEntityQueryableRepository<Log>
    {
    }
}
