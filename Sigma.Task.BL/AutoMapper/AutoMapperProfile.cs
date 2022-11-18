using AutoMapper;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;

namespace Sigma.Task.BL;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<CandidateWriteDTO, Candidate>();
	}
}
