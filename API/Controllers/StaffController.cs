using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
    {
        var staffs = await context.Staffs.ToListAsync();
        return staffs;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Staff>> GetStaff(int id)
    {
        var staff = await context.Staffs.FindAsync(id);
        if (staff == null) return NotFound();
        return Ok(staff);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStaff(int id, StaffUpdateDto updatedStaff)
    {
        var existingStaff = await context.Staffs.FindAsync(id);
        if (existingStaff == null)
            return NotFound();

        existingStaff.FullName = updatedStaff.FullName;
        existingStaff.JointYearMonth = updatedStaff.JointYearMonth;
        existingStaff.UserName = updatedStaff.UserName;

        if (!string.IsNullOrEmpty(updatedStaff.Password))
        {
            using var hmac = new HMACSHA512();
            existingStaff.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(updatedStaff.Password));
            existingStaff.PasswordSalt = hmac.Key;
        }

        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var staff = await context.Staffs.FindAsync(id);
        if (staff == null) return NotFound();

        context.Staffs.Remove(staff);
        await context.SaveChangesAsync();
        return NoContent();
    }

}
