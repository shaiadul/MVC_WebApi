using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testing.Data;
using testing.Models;


namespace testing.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<IActionResult> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return Ok(comments);
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comments comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comments comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }
            
            existingComment.Date = comment.Date;
            await _context.SaveChangesAsync();
            return Ok(existingComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return Ok(existingComment);
        }
    }
}