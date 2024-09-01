using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;


namespace testing.Controllers
{
    [Route("api/examspack")]
    [ApiController]
    public class ExamsPackController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ExamsPackController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamsPacks()
        {
            var examsPacks = await _context.ExamsPacks.ToListAsync();
            return Ok(examsPacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamsPackById(int id)
        {
            var examsPack = await _context.ExamsPacks.FindAsync(id);
            if (examsPack == null)
            {
                return NotFound();
            }
            return Ok(examsPack);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamsPack(ExamsPack examsPack)
        {
            _context.ExamsPacks.Add(examsPack);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExamsPackById), new { id = examsPack.Id }, examsPack);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamsPack(int id, ExamsPack examsPack)
        {
            if (id != examsPack.Id)
            {
                return BadRequest();
            }

            _context.Entry(examsPack).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamsPack(int id)
        {
            var examsPack = await _context.ExamsPacks.FindAsync(id);
            if (examsPack == null)
            {
                return NotFound();
            }

            _context.ExamsPacks.Remove(examsPack);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
