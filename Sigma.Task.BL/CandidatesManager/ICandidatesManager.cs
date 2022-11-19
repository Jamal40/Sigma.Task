using Sigma.Task.DAL;
using Sigma.Task.DTOs;

namespace Sigma.Task.BL;

public interface ICandidatesManager
{
    List<CandidateReadDTO>? GetAll();
    bool AddOrUpdate(CandidateWriteDTO candidateDTO);
}
