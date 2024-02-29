using APITracking.Data;
using APITracking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APITracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _context;

        public IssueController(IssueDbContext context)
        {
            this._context = context;
        }

        
        //Get the list of issues
        [HttpGet]
        public async Task<IEnumerable<Issue>> Get() 
            => await _context.issues.ToListAsync();

        //Get an issue with its id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id) 
        {
            var issue = await _context.issues.FindAsync(id);
            return issue == null ? NotFound() :  Ok(issue); 
        }

        //create an issue
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateIssue(Issue issue)
        {
            await _context.issues.AddAsync(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
        }

        //Update an issue
        [HttpPut("{id}")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAnIssue(int id, Issue issue)
        {
            if(id != issue.Id) return BadRequest(); 

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        //deleting an issue
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAnIssue(int id)
        {
            var issueToDelete = await _context.issues.FindAsync(id);
            if(issueToDelete == null) return NotFound();

            _context.issues.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
