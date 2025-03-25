using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Data;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            // Returns all tasks from the database
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(string id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // PATCH: api/Tasks/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<Models.Task>> PatchTask(string id, Models.Task taskUpdate)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            // Update only non-null properties
            if (!string.IsNullOrEmpty(taskUpdate.Title))
                task.Title = taskUpdate.Title;
            
            if (!string.IsNullOrEmpty(taskUpdate.Description))
                task.Description = taskUpdate.Description;
            
            if (!string.IsNullOrEmpty(taskUpdate.Status))
                task.Status = taskUpdate.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return the complete updated task
            return task;
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            // Check for duplicate title
            bool duplicateExists = await _context.Tasks.AnyAsync(t =>
                t.Title.ToLower().Trim() == task.Title.ToLower().Trim());

            if (duplicateExists)
            {
                return BadRequest("A task with this title already exists. Please use a different title.");
            }

            // If id is not provided, generate a random one
            if (string.IsNullOrEmpty(task.Id))
            {
                task.Id = new Random().Next(1000, 10000).ToString();
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(string id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
