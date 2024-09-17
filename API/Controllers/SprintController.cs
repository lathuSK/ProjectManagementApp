using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SprintController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sprint>>> GetSprints()
    {
        var sprints = await context.Sprints.ToListAsync();
        return sprints;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Sprint>> GetSprint(int id)
    {
        var sprint = await context.Sprints.FindAsync(id);
        if (sprint == null) return NotFound();
        return Ok(sprint);
    }

    [HttpPost]
    public async Task<ActionResult<Sprint>> CreateSprint(Sprint sprint)
    {
        context.Sprints.Add(sprint);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSprint), new { id = sprint.Id }, sprint);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSprint(int id, Sprint updatedSprint)
    {
        if (id != updatedSprint.Id)
            return BadRequest("ID mismatch");

        var existingSprint = await context.Sprints.FindAsync(id);
        if (existingSprint == null)
            return NotFound();

        existingSprint.ProjectId = updatedSprint.ProjectId;
        existingSprint.SprintName = updatedSprint.SprintName;
        existingSprint.SprintDetails = updatedSprint.SprintDetails;
        existingSprint.SprintDocumentName = updatedSprint.SprintDocumentName;
        existingSprint.AllocatedDays = updatedSprint.AllocatedDays;
        existingSprint.ExpectDate = updatedSprint.ExpectDate;
        existingSprint.StartDate = updatedSprint.StartDate;
        existingSprint.EndDate = updatedSprint.EndDate;
        existingSprint.ApprovedBy = updatedSprint.ApprovedBy;
        existingSprint.AllocatedTo = updatedSprint.AllocatedTo;
        existingSprint.TestedBy = updatedSprint.TestedBy;
        existingSprint.IsCompleted = updatedSprint.IsCompleted;
        existingSprint.Complains = updatedSprint.Complains;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSprint(int id)
    {
        var sprint = await context.Sprints.FindAsync(id);
        if (sprint == null) return NotFound();

        context.Sprints.Remove(sprint);
        await context.SaveChangesAsync();
        return NoContent();
    }
}

