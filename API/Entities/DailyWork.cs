using System;

namespace API.Entities;

public class DailyWork
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public DateTime JobDate { get; set; }
    public int ProjectId { get; set; }
    public int SprintId { get; set; }
    public int TaskId { get; set; }
    public required string YourWork { get; set; }
    public required string Status { get; set; }
    public required string Difficulty { get; set; }
    public required string WaitingFor { get; set; }
    public required string Requirement { get; set; }
}
