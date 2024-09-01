using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;


namespace testing.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ExamsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            var exams = await _context.Exams.ToListAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamsById(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            if (exams == null)
            {
                return NotFound();
            }
            return Ok(exams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExams(Exams exams)
        {
            _context.Exams.Add(exams);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExamsById), new { id = exams.Id }, exams);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExams(int id, Exams exams)
        {
            if (id != exams.Id)
            {
                return BadRequest();
            }

            _context.Entry(exams).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExams(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            if (exams == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exams);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
