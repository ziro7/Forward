using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForwardBackend.Models;
using Core;
using Microsoft.Extensions.Logging;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace ForwardBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<JobsController> _logger;

        public JobsController(AppDbContext context, ILogger<JobsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Jobs
        [HttpGet]
        //[EnableQuery()]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "GetJobs Called");
            var jobs = await _context.Jobs
                .Include(j => j.WorkExperiences)
                .ToListAsync()
                .ConfigureAwait(true);

            if (jobs == null) {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, "GetJobs Called but failed and returned NotFound");
                return NotFound();
            }

            return jobs;
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting job {Id}", id);
            var job = await _context.Jobs
                .Include(j => j.WorkExperiences)
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JobId == id)
                .ConfigureAwait(true);

            if (job == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetJob({Id}) NOT FOUND", id);
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "Putjob {Id}", id);
            if (job == null)
            {
                throw new ArgumentNullException("The job with id: " + id + " is null");
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            if (job is null) {
                throw new ArgumentNullException(nameof(job));
            }

            _logger.LogInformation(LoggingEvents.UpdateItem, "PostJob from company {0}", job.CompanyName);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteItem, "PostJob from company {id}", id);
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return job;
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
