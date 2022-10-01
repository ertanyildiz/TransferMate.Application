using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TaskController> _log;

        public TaskController(IConfiguration configuration, ILogger<TaskController> log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Creates a customer object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///      {
        ///        "CreatedDate": "2022-10-01 19:03:37.017",
        ///        "RequiredByDate": "2022-10-01 19:03:37.017",
        ///        "TaskDescription": "Sample Task Description",
        ///        "TaskStatus": 1,
        ///        "TaskType": 1,
        ///        "AssignedTo": 1,
        ///        "NextActionDate": "2022-10-01 19:03:37.017"
        ///      }
        ///
        /// </remarks>
        /// <param name="customer">Customer object that is going to be created</param>
        /// <returns>Created Customer</returns>
        /// <response code="200">Returns the updated Customer</response>
        /// <response code="400">If Customer validation is failed</response>
        [HttpPost("CreateTask")]
        public async Task<IActionResult> Create([FromBody] Data.Models.Task task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
                taskRepository.Update(task);
                return Ok(task);
            }
            catch (Exception ex)
            {
                _log.LogError($"[CustomerController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }
    }
}
