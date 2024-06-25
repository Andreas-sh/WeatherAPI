// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API.models.weatherapi;
using Web_API.Services;

[ApiController]
[Route("api/[controller]/[Action]")]
public class TodoController : ControllerBase
{
    private readonly DBcontext _context;

    public TodoController(DBcontext context)
    {
        _context = context;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherItem>>> GetTodoItems()
    {
        return await _context.WeatherItems.ToListAsync();
    }

    // GET: api/Todo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherItem>> GetTodoItem(long id)
    {
        var todoItem = await _context.WeatherItems.FindAsync(id);

        if(todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }

    // POST: api/Todo
   [HttpPost]
    public async Task<ActionResult<WeatherItem>> PostTodoItem(WeatherItem todoItem)
    {
        _context.WeatherItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }

    // PUT: api/Todo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(long id, WeatherItem todoItem)
    {
        if(id != todoItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!TodoItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Todo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.WeatherItems.FindAsync(id);
        if(todoItem == null)
        {
            return NotFound();
        }

        _context.WeatherItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(long id)
    {
        return _context.WeatherItems.Any(e => e.Id == id);
    }
        // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AstronomyItem>>> GetAstronomyItems()
    {
        return await _context.AstronomyItems.ToListAsync();
    }

    // GET: api/Todo/5
    // POST: api/Todo
    [HttpPost]
    public async Task<ActionResult<AstronomyItem>> PostAstronomyItem(AstronomyItem todoItem)
    {
        _context.AstronomyItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItem), new { sunriseTime = todoItem.Id }, todoItem);
    }

    // PUT: api/Todo/5
  

    // DELETE: api/Todo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAstronomyItem(long id)
    {
        var todoItem = await _context.AstronomyItems.FindAsync(id);
        if(todoItem == null)
        {
            return NotFound();
        }

        _context.AstronomyItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AstronomyItemExists(long id)
    {
        return _context.AstronomyItems.Any(e => e.Id == id);
    }
}