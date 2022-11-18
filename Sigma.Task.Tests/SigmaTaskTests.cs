using Microsoft.AspNetCore.Mvc;
using Moq;
using Sigma.Task.BL;
using Sigma.Task.DAL;
using Sigma.Task.DTOs;
using Sigma.Task.PL.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.Tests;

public class CandidatesControllerTests
{
    private Mock<ICandidatesManager> _candidatesManager;
    private CandidatesController? _candidatesController;

    public CandidatesControllerTests()
    {
        _candidatesManager = new();
    }

    [Fact]
    public void Add_Or_Update_Endpoint_Returns_NoContent()
    {
        //Arange
        var candidate = GenerateValidCandidateDTO();
        _candidatesManager
            .Setup(repo => repo.AddOrUpdate(candidate))
            .Returns<CandidateWriteDTO>((c) => true);
        _candidatesController = new CandidatesController(_candidatesManager.Object);

        //Act
        var result = _candidatesController.AddOrUpdate(candidate);

        //Assert
        Assert.True(result is NoContentResult);
    }

    [Fact]
    public void CandidateWriteDTO_Email_Must_Be_Valid()
    {
        //Arange
        var validCandidate = GenerateValidCandidateDTO();
        var invalidCandidate = validCandidate with { Email = "InvalidEmail" };

        //Act

        //Assert
        Assert.Contains(ValidateModel(invalidCandidate),
            error => error.MemberNames.Contains("Email"));
    }

    #region Helpers

    private static CandidateWriteDTO GenerateValidCandidateDTO()
    {
        return new CandidateWriteDTO
        {
            Email = "dummy@outlook.com",
            FirstName = "Fake First Name",
            LastName = "Fake Last Name",
            Comment = "Dummy Coment"
        };
    }

    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }

    #endregion
}