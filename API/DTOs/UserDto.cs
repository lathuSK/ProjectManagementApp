using System;

namespace API.DTOs;

public class UserDto
{
    public required string Username { get; set; }
    public required string FullName { get; set; }
    public required string JointYearMonth { get; set; }
    public required string Token { get; set; }

}

