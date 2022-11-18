using AutoMapper;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;

namespace Sigma.Task.BL.CandidatesManager;

public class CandidatesManager : ICandidatesManager
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IMapper _mapper;

    public CandidatesManager(ICandidateRepository candidateRepository, IMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
    }

    public bool AddOrUpdate(CandidateWriteDTO candidateDTO)
    {
        try
        {
            var model = _mapper.Map<Candidate>(candidateDTO);
            _candidateRepository.AddOrUpdate(model);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
