using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DailyWorkController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailyWork>>> GetDailyWorks()
    {
        var dailyWork = await context.DailyWorks.ToListAsync();
        return dailyWork;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DailyWork>> GetDailyWork(int id)
    {
        var dailyWork = await context.DailyWorks.FindAsync(id);
        if (dailyWork == null) return NotFound();
        return Ok(dailyWork);
    }

    [HttpPost]
        public async Task<ActionResult<DailyWork>> CreateDailyWork(DailyWork dailyWork)
        {
            context.DailyWorks.Add(dailyWork);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDailyWork), new { id = dailyWork.Id }, dailyWork);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDailyWork(int id, DailyWork updatedDailyWork)
        {
            if (id != updatedDailyWork.Id)
                return BadRequest("ID mismatch");

            var existingDailyWork = await context.DailyWorks.FindAsync(id);
            if (existingDailyWork == null)
                return NotFound();

            existingDailyWork.StaffId = updatedDailyWork.StaffId;
            existingDailyWork.JobDate = updatedDailyWork.JobDate;
            existingDailyWork.ProjectId = updatedDailyWork.ProjectId;
            existingDailyWork.SprintId = updatedDailyWork.SprintId;
            existingDailyWork.TaskId = updatedDailyWork.TaskId;
            existingDailyWork.YourWork = updatedDailyWork.YourWork;
            existingDailyWork.Status = updatedDailyWork.Status;
            existingDailyWork.Difficulty = updatedDailyWork.Difficulty;
            existingDailyWork.WaitingFor = updatedDailyWork.WaitingFor;
            existingDailyWork.Requirement = updatedDailyWork.Requirement;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDailyWork(int id)
        {
            var dailyWork = await context.DailyWorks.FindAsync(id);
            if (dailyWork == null) return NotFound();

            context.DailyWorks.Remove(dailyWork);
            await context.SaveChangesAsync();
            return NoContent();
        }
}

