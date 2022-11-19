using Microsoft.EntityFrameworkCore;

namespace Sigma.Task.DAL;

public class CandidatesContext : DbContext
{
    public DbSet<Candidate> Candidates => Set<Candidate>();

    public CandidatesContext(DbContextOptions<CandidatesContext> options) : base(options)
    {
    }
}
