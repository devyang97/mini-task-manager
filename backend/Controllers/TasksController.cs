using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/tasks - get all tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskManagerAPI.Models.Task>>> GetTasks()
        {
            return await _context.Tasks
                .Include(t => t.User)
                .ToListAsync();
        }

        //GET api/tasks/1 - get one tasks by Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskManagerAPI.Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include( t => t.User)
                .FirstOrDefaultAsync( t => t.Id == id);
            
            if (task == null) return NotFound();
            return task;
        }

        // POST api/tasks — create new task
        [HttpPost]
        public async Task<ActionResult<TaskManagerAPI.Models.Task>> CreateTask(TaskManagerAPI.Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT api/tasks/1 — update task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskManagerAPI.Models.Task task)
        {
            if (id != task.Id) return BadRequest();
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/tasks/1 — delete task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}