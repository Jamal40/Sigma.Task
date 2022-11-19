using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Sigma.Task.BL.Utilities;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;

namespace Sigma.Task.BL.CandidatesManager;

public class CandidatesManager : ICandidatesManager
{
    private readonly ICandidatesRepository _candidateRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public CandidatesManager(ICandidatesRepository candidateRepository, IMapper mapper, IMemoryCache memoryCache)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public List<CandidateReadDTO>? GetAll()
    {
        try
        {
            Console.WriteLine("Method Executed");
            var result = _mapper.Map<List<CandidateReadDTO>>(_candidateRepository.GetAll());
            _memoryCache.Set(CachingKeys.Candidates, result, TimeSpan.FromMinutes(20));
            return result;
        }
        catch
        {
            return null;
        }
    }

    public bool AddOrUpdate(CandidateWriteDTO candidateDTO)
    {
        try
        {
            var model = _mapper.Map<Candidate>(candidateDTO);
            _candidateRepository.AddOrUpdate(model);
            _candidateRepository.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
