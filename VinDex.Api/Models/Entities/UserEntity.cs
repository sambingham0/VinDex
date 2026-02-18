using System.ComponentModel.DataAnnotations;

namespace VinDex.Api.Data.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string Email { get; set; }
    public string? Name { get; set; }
    public string? GoogleSubjectId { get; set; }
}