namespace Sigma.Task.DAL;

public class EFCandidatesRepository : EFGenericRepository<Candidate>, ICandidatesRepository
{
    public EFCandidatesRepository(CandidatesContext context) : base(context)
    {
    }
}
