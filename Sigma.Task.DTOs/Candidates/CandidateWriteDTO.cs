using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.DTOs;

public record CandidateWriteDTO
{
    [EmailAddress]
    [Required]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string FirstName { get; init; } = string.Empty;

    [Required]
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string CallTimeInterval { get; init; } = string.Empty;

    [Url]
    public string LinkedInProfile { get; init; } = string.Empty;

    [Url]
    public string GitHubProfile { get; init; } = string.Empty;

    [Required]
    public string Comment { get; init; } = string.Empty;
}
