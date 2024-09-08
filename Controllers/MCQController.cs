using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;
using System.Threading.Tasks;

namespace testing.Controllers
{
    [Route("api/mcq")]
    [ApiController]
    public class MCQController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MCQController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMCQs()
        {
            var mcqs = await _context.MCQs.Include(m => m.MultipleSelection).ToListAsync();
            return Ok(mcqs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMCQById(int id)
        {
            var mcq = await _context.MCQs.Include(m => m.MultipleSelection).FirstOrDefaultAsync(m => m.Id == id);
            if (mcq == null)
            {
                return NotFound();
            }
            return Ok(mcq);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMCQ([FromBody] MCQ mcq)
        {
            _context.MCQs.Add(mcq);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMCQById), new { id = mcq.Id }, mcq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMCQ(int id, [FromBody] MCQ mcq)
        {
            if (id != mcq.Id)
            {
                return BadRequest();
            }

            _context.Entry(mcq).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMCQ(int id)
        {
            var mcq = await _context.MCQs.FindAsync(id);
            if (mcq == null)
            {
                return NotFound();
            }

            _context.MCQs.Remove(mcq);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Add MultipleQuestion to MCQ
        [HttpPost("{mcqId}/add-multiple-question")]
        public async Task<IActionResult> AddMultipleQuestion(int mcqId, [FromBody] MultipleQuestion multipleQuestion)
        {
            var mcq = await _context.MCQs.Include(m => m.MultipleSelection).FirstOrDefaultAsync(m => m.Id == mcqId);
            if (mcq == null)
            {
                return NotFound("MCQ not found");
            }

            mcq.MultipleSelection.Add(multipleQuestion);
            await _context.SaveChangesAsync();
            return Ok(mcq);
        }
    }
}
