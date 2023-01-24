using Core.DataAccess.Mongo;
using Core.Entities.Concrete;
using SBA.DataAccess.Abstract;

namespace SBA.DataAccess.Concrete.MongoDB
{
    public class MngFilterResultDal : MongoBaseRepository<FilterResult>, IFilterResultDal
    {
        public MngFilterResultDal() : base("FilterResults")
        {
        }
    }
}
