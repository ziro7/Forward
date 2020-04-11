using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForwardBackend.Models;
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
                .ConfigureAwait(false);

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

            /*
             Had some issue with updating the list of workexperiences, as the job seemed to be tracked without the link to workexperiences.
             If i tried saving it, it failed with "The instance of entity type cannot be tracked because another instance of this type with 
             the same key is already being tracked".
             I decided to do an approach where i find the job and work experiences and detach it from tracking.
             Then set the specified ones i need to modified.
             And lastly save the changes.
             */

            // Getting the job to update from database
            var jobInDb = await _context.Jobs
              .Include(j => j.WorkExperiences)
              .AsNoTracking()
              .FirstOrDefaultAsync(j => j.JobId == id)
              .ConfigureAwait(false);

            // It is allready being tracked so i detach it.
            _context.Entry(jobInDb).State = EntityState.Detached;

            foreach (var experience in jobInDb.WorkExperiences) {
                var workxp = await _context.WorkExperiences
                    .AsNoTracking()
                    .FirstOrDefaultAsync(w => w.JobForeignKey == job.JobId)
                    .ConfigureAwait(false);
                _context.Entry(workxp).State = EntityState.Detached;
            }

            //Modifing the items i need to update
            try {
                // current experiences in db
                foreach (var workxp in jobInDb.WorkExperiences) {
                    _context.Entry(workxp).State = EntityState.Modified;
                }
                // new added experiences
                if (job.WorkExperiences.Count > jobInDb.WorkExperiences.Count) {
                    _context.WorkExperiences.Add(job.WorkExperiences[job.WorkExperiences.Count - 1]);
                }
                // data on the job it self
                _context.Entry(job).State = EntityState.Modified;
            } catch (InvalidOperationException ex) {
                Console.WriteLine(ex.StackTrace);
            }

            // Save
            try {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            } catch (DbUpdateConcurrencyException) {
                if (!JobExists(id)) {
                    return NotFound();
                } else {
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
            await _context.SaveChangesAsync().ConfigureAwait(false);

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
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return job;
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
