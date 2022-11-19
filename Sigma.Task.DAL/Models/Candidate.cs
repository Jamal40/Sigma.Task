using System.ComponentModel.DataAnnotations;

namespace Sigma.Task.DAL;

public class Candidate
{
    [ExcelKey]
    [Key]
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CallTimeInterval { get; set; } = string.Empty;
    public string LinkedInProfile { get; set; } = string.Empty;
    public string GitHubProfile { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}
