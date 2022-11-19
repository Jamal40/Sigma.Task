using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.DTOs;

public record CandidateReadDTO
{
    public string Email { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string CallTimeInterval { get; init; } = string.Empty;
    public string LinkedInProfile { get; init; } = string.Empty;
    public string GitHubProfile { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
}
