using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;
using System.Threading.Tasks;
using System.Linq;

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
            var examsPacks = await _context.ExamsPacks.Include(e => e.Exams).ThenInclude(m => m.MCQs).ToListAsync();
            return Ok(examsPacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamsPackById(int id)
        {
            var examsPack = await _context.ExamsPacks
                .Include(e => e.Exams).ThenInclude(m => m.MCQs)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (examsPack == null)
            {
                return NotFound();
            }
            return Ok(examsPack);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamsPack([FromBody] ExamsPack examsPack)
        {
            _context.ExamsPacks.Add(examsPack);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExamsPackById), new { id = examsPack.Id }, examsPack);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamsPack(int id, [FromBody] ExamsPack examsPack)
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

        // Add an Exam to ExamsPack
        [HttpPost("{examsPackId}/add-exam")]
        public async Task<IActionResult> AddExam(int examsPackId, [FromBody] Exams exam)
        {
            var examsPack = await _context.ExamsPacks.Include(e => e.Exams).FirstOrDefaultAsync(e => e.Id == examsPackId);

            if (examsPack == null)
            {
                return NotFound("ExamsPack not found");
            }

            examsPack.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return Ok(examsPack);
        }
    }
}
