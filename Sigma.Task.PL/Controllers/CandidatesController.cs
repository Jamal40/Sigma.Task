using Microsoft.AspNetCore.Mvc;
using Sigma.Task.BL;
using Sigma.Task.BL.CandidatesManager;
using Sigma.Task.BL.Utilities;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;
using Sigma.Task.PL.Filters;

namespace Sigma.Task.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly ICandidatesManager _candidatesManager;

    public CandidatesController(ICandidatesManager candidatesManager)
    {
        _candidatesManager = candidatesManager;
    }

    [HttpGet]
    [TypeFilter(typeof(CachingFilterResourceAttribute), Arguments = new[] { CachingKeys.Candidates })]
    public ActionResult<List<CandidateReadDTO>> GetAll()
    {
        var result = _candidatesManager.GetAll();
        if (result is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return result;
    }

    [HttpPost]
    public ActionResult AddOrUpdate(CandidateWriteDTO input)
    {
        var isSuccess = _candidatesManager.AddOrUpdate(input);
        if (!isSuccess)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return NoContent();
    }
}
