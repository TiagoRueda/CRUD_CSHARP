using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApiProject.Data;
using UserApiProject.Models;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // Obter todos os usuarios
    [HttpGet("/users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(new { users });
    }

    // Obter usuario por ID
    [HttpGet("/user/{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(new { user });
    }

    // Criar usuario
    [HttpPost("/user")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Created("", new { user, message = "Successfully created" });
        }
        catch
        {
            return BadRequest(new { message = "Error creating user" });
        }
    }

    // Atualizar usuario
    [HttpPut("/user/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        user.Name = updatedUser.Name ?? user.Name;
        user.Email = updatedUser.Email ?? user.Email;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(new { user, message = "Successfully updated" });
        }
        catch
        {
            return BadRequest(new { message = "Error updating user" });
        }
    }

    // Deletar usuario
    [HttpDelete("/user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound(new { message = "User not found" });

        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(new { user, message = "Successfully deleted" });
        }
        catch
        {
            return BadRequest(new { message = "Error deleting user" });
        }
    }
}
