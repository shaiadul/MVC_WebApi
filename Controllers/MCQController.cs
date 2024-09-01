using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Models;
using testing.Data;


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
            var mcqs = await _context.MCQs.ToListAsync();
            return Ok(mcqs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMCQById(int id)
        {
            var mcq = await _context.MCQs.FindAsync(id);
            if (mcq == null)
            {
                return NotFound();
            }
            return Ok(mcq);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMCQ(MCQ mcq)
        {
            _context.MCQs.Add(mcq);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMCQById), new { id = mcq.Id }, mcq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMCQ(int id, MCQ mcq)
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
    }
}
