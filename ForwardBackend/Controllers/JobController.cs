using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForwardBackend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForwardBackend.Controllers
{
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository) {
            _jobRepository = jobRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Job> Get() {

            var jobs = _jobRepository.GetAllJobs().OrderBy(j => j.EndDate);

            return jobs;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
