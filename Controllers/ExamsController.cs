using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;
using System.Threading.Tasks;

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
            var exams = await _context.Exams.Include(e => e.MCQs).ToListAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamsById(int id)
        {
            var exams = await _context.Exams.Include(e => e.MCQs).FirstOrDefaultAsync(e => e.Id == id);
            if (exams == null)
            {
                return NotFound();
            }
            return Ok(exams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExams([FromBody] Exams exams)
        {
            _context.Exams.Add(exams);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExamsById), new { id = exams.Id }, exams);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExams(int id, [FromBody] Exams exams)
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

        // Add MCQ to Exams
        [HttpPost("{examId}/add-mcq")]
        public async Task<IActionResult> AddMCQ(int examId, [FromBody] MCQ mcq)
        {
            var exam = await _context.Exams.Include(e => e.MCQs).FirstOrDefaultAsync(e => e.Id == examId);
            if (exam == null)
            {
                return NotFound("Exam not found");
            }

            exam.MCQs.Add(mcq);
            await _context.SaveChangesAsync();
            return Ok(exam);
        }
    }
}
