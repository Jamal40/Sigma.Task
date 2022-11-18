using Sigma.Task.DTOs;

namespace Sigma.Task.BL;

public interface ICandidatesManager
{
    bool AddOrUpdate(CandidateWriteDTO candidateDTO);
}
