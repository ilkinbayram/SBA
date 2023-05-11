using Core.DataAccess;
using Core.Entities.Concrete.ComplexModels.Program;
using Core.Entities.Concrete.ExternalDbEntities;

namespace SBA.ExternalDataAccess.Abstract
{
    public interface IMatchIdentifierDal : IEntityRepository<MatchIdentifier>, IEntityQueryableRepository<MatchIdentifier>
    {
        MatchProgramList GetGroupedMatchsProgram();
        MatchDetailProgram GetAllMatchsProgram();
        Task<MatchProgramList> GetGroupedMatchsProgramAsync();
        Task<MatchDetailProgram> GetAllMatchsProgramAsync();
    }
}
