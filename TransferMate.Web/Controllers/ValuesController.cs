using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
            var taskModel = new Data.Models.Task
            {
                CreatedDate = DateTime.Now,
                AssignedTo = 1,
                NextActionDate = DateTime.Now,
                RequiredByDate = DateTime.Now,
                TaskDescription = "updated",
                TaskStatus = 1,
                TaskType = 1,
                Id = 2
            };
            taskRepository.Update(taskModel);
            taskRepository.Select(2);
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
