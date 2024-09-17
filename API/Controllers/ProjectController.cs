using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        var projects = await context.Projects.ToListAsync();
        return projects;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(Project project)
    {
        context.Projects.Add(project);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProject(int id, Project updatedProject)
    {
        if (id != updatedProject.Id)
            return BadRequest("ID mismatch");

        var existingProject = await context.Projects.FindAsync(id);
        if (existingProject == null)
            return NotFound();

        existingProject.Name = updatedProject.Name;
        existingProject.Detail = updatedProject.Detail;
        existingProject.WebHostedDetails = updatedProject.WebHostedDetails;
        existingProject.DatabaseDetails = updatedProject.DatabaseDetails;
        existingProject.MainDeveloper = updatedProject.MainDeveloper;
        existingProject.SupportDevelopers = updatedProject.SupportDevelopers;
        existingProject.AzureDevopsDetails = updatedProject.AzureDevopsDetails;
        existingProject.CodeLocationInVM = updatedProject.CodeLocationInVM;
        existingProject.LinkedProjects = updatedProject.LinkedProjects;

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null) return NotFound();

        context.Projects.Remove(project);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
