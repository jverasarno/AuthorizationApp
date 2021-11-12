using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.Models;
using TodoWebAPI.Common;
using Microsoft.AspNetCore.Authorization;

namespace TodoWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoesController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            
            var todoes = await _context.Todo.ToListAsync();
         
            return todoes;
        }

        // GET: api/Todoes/5
        [HttpGet("{details}")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo(int id, string userrole, int userid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todoes = await _context.Todo.ToListAsync();

            if (userrole != null && userrole.Length > 0)
            {
                switch (userrole)
                {
                    case "VPTI":
                        todoes = todoes.Where(e => e.IsDoneByUser == true && e.IsDoneByVPP == true && e.IsDoneByCI == true && e.VPTIUser == userid).ToList();
                        break;
                    case "NU":
                        break;
                    case "CI":
                        todoes = todoes.Where(e => e.IsDoneByUser == true && e.IsDoneByVPP == true && e.CIUser == userid).ToList();
                        break;
                    case "VPP":
                        todoes = todoes.Where(e => e.IsDoneByUser == true && e.VPPUser == userid).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (id > 0)
                todoes = todoes.Where(e => e.Id == id).ToList();


            if (todoes == null)
            {
                return NotFound();
            }

            return todoes;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
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

        // POST: api/Todoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}
