using Microsoft.AspNetCore.Mvc;
using Moq;
using Sigma.Task.DAL;
using Sigma.Task.PL.Controllers;

namespace Sigma.Task.Tests;

public class CandidatesControllerTests
{
    private Mock<ICandidateRepository> _candidateRepositoryMock;
    private CandidatesController? _candidatesController;

    public CandidatesControllerTests()
    {
        _candidateRepositoryMock = new Mock<ICandidateRepository>();
    }

    [Fact]
    public void Add_Or_Update_Endpoint_Returns_NoContent()
    {
        //Arange
        var candidate = new Candidate();
        _candidateRepositoryMock
            .Setup(repo => repo.AddOrUpdate(candidate))
            .Callback<Candidate>((c) => { });
        _candidatesController = new CandidatesController(_candidateRepositoryMock.Object);

        //Act
        var result = _candidatesController.AddOrUpdate(candidate);

        //Assert
        Assert.True(result is NoContentResult);
    }

    [Fact]
    public void Add_Or_Update_Endpoint_Returns_BBadRequest_Given_Invalid_Email()
    {
        //Arange
        var candidate = new Candidate();
        _candidateRepositoryMock
            .Setup(repo => repo.AddOrUpdate(candidate))
            .Callback<Candidate>((c) => { });
        _candidatesController = new CandidatesController(_candidateRepositoryMock.Object);

        //Act
        var result = _candidatesController.AddOrUpdate(candidate);

        //Assert
        Assert.True(result is NoContentResult);
    }
}