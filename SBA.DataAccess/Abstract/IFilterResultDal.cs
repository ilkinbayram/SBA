using Core.DataAccess;
using Core.Entities.Concrete;

namespace SBA.DataAccess.Abstract
{
    public interface IFilterResultDal : IEntityRepository<FilterResult>, IEntityQueryableRepository<FilterResult>
    {
    }
}
