using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.DTOs;

public record CandidateWriteDTO
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CallTimeInterval { get; set; } = string.Empty;

    [Url]
    public string LinkedInProfile { get; set; } = string.Empty;

    [Url]
    public string GitHubProfile { get; set; } = string.Empty;

    [Required]
    public string Comment { get; set; } = string.Empty;
}
