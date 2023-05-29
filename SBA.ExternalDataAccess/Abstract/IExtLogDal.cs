using Core.DataAccess;
using Core.Entities.Concrete;

namespace SBA.DataAccess.Abstract
{
    public interface IExtLogDal : IEntityRepository<Log>, IEntityQueryableRepository<Log>
    {
    }
}
