using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public required string Username  { get; set; } = string.Empty;
    [Required]  
     [StringLength(12, MinimumLength = 8)]  
    public required string Password { get; set; } = string.Empty;
    [Required]
    public required string FullName { get; set; } = string.Empty;
    [Required]
    public required string JointYearMonth { get; set; } = string.Empty;


}
