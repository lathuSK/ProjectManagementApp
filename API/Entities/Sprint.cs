using System;

namespace API.Entities;

public class Sprint
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public required string SprintName { get; set; }
    public required string SprintDetails { get; set; }
    public required string SprintDocumentName { get; set; }
    public required int AllocatedDays { get; set; }
    public required DateTime ExpectDate { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required int ApprovedBy { get; set; }
    public required int AllocatedTo { get; set; }
    public required int TestedBy { get; set; }
    public required bool IsCompleted { get; set; }
    public required string Complains { get; set; }
}
