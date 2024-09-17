using System;

namespace API.Entities;

public class Task
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int SprintId { get; set; }
    public required string TaskName { get; set; }
    public int AllocatedTo { get; set; }
    public int TestedBy { get; set; }
    public bool IsCompleted { get; set; }
    public required string Complains { get; set; }
}

