using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core;
using ForwardBackend.Models;

namespace ForwardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkExperiencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkExperiencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkExperiences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkExperience>>> GetWorkExperiences()
        {
            return await _context.WorkExperiences.ToListAsync();
        }

        // GET: api/WorkExperiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperience>> GetWorkExperience(int id)
        {
            var workExperience = await _context.WorkExperiences.FindAsync(id);

            if (workExperience == null)
            {
                return NotFound();
            }

            return workExperience;
        }

        // PUT: api/WorkExperiences/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkExperience(int id, WorkExperience workExperience)
        {
            if (id != workExperience.Id)
            {
                return BadRequest();
            }

            _context.Entry(workExperience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExperienceExists(id))
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

        // POST: api/WorkExperiences
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WorkExperience>> PostWorkExperience(WorkExperience workExperience)
        {
            _context.WorkExperiences.Add(workExperience);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkExperience", new { id = workExperience.Id }, workExperience);
        }

        // DELETE: api/WorkExperiences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkExperience>> DeleteWorkExperience(int id)
        {
            var workExperience = await _context.WorkExperiences.FindAsync(id);
            if (workExperience == null)
            {
                return NotFound();
            }

            _context.WorkExperiences.Remove(workExperience);
            await _context.SaveChangesAsync();

            return workExperience;
        }

        private bool WorkExperienceExists(int id)
        {
            return _context.WorkExperiences.Any(e => e.Id == id);
        }
    }
}
