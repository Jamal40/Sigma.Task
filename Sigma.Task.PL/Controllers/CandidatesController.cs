using Microsoft.AspNetCore.Mvc;
using Sigma.Task.BL;
using Sigma.Task.BL.CandidatesManager;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;

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
