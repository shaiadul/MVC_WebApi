using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;


namespace testing.Controllers
{
    [Route("api/multiplequestion")]
    [ApiController]

    public class MultipleQuestionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MultipleQuestionController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMultipleQuestions()
        {
            var multipleQuestions = await _context.MultipleQuestions.ToListAsync();
            return Ok(multipleQuestions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMultipleQuestionById(int id)
        {
            var multipleQuestion = await _context.MultipleQuestions.FindAsync(id);
            if (multipleQuestion == null)
            {
                return NotFound();
            }
            return Ok(multipleQuestion);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultipleQuestion(MultipleQuestion multipleQuestion)
        {
            _context.MultipleQuestions.Add(multipleQuestion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMultipleQuestionById), new { id = multipleQuestion.Id }, multipleQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMultipleQuestion(int id, MultipleQuestion multipleQuestion)
        {
            if (id != multipleQuestion.Id)
            {
                return BadRequest();
            }

            _context.Entry(multipleQuestion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMultipleQuestion(int id)
        {
            var multipleQuestion = await _context.MultipleQuestions.FindAsync(id);
            if (multipleQuestion == null)
            {
                return NotFound();
            }

            _context.MultipleQuestions.Remove(multipleQuestion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
