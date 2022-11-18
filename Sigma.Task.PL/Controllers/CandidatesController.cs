using Microsoft.AspNetCore.Mvc;
using Sigma.Task.DAL;

namespace Sigma.Task.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
	private readonly ICandidateRepository _candidateRepository;

	public CandidatesController(ICandidateRepository candidateRepository)
	{
		_candidateRepository = candidateRepository;
	}

	[HttpPost]
	public ActionResult AddOrUpdate(Candidate candidate)
	{
		_candidateRepository.AddOrUpdate(candidate);
		return NoContent();
	}
}
