using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entities.Task>>> GetTasks()
    {
        var tasks = await context.Tasks.ToListAsync();
        return tasks;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Entities.Task>> GetTask(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<Entities.Task>> CreateTask(Entities.Task task)
    {
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, Entities.Task updatedTask)
    {
        if (id != updatedTask.Id)
            return BadRequest("ID mismatch");

        var existingTask = await context.Tasks.FindAsync(id);
        if (existingTask == null)
            return NotFound();

        existingTask.ProjectId = updatedTask.ProjectId;
        existingTask.SprintId = updatedTask.SprintId;
        existingTask.TaskName = updatedTask.TaskName;
        existingTask.AllocatedTo = updatedTask.AllocatedTo;
        existingTask.TestedBy = updatedTask.TestedBy;
        existingTask.IsCompleted = updatedTask.IsCompleted;
        existingTask.Complains = updatedTask.Complains;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
