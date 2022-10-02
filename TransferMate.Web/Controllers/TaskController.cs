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
        /// Creates a Task object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/CreateTask
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
        /// <param name="task">Task object that is going to be created</param>
        /// <returns>Created Task</returns>
        /// <response code="200">Returns the inserted Task</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpPost("CreateTask")]
        public IActionResult Create([FromBody] Data.Models.Task task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
                taskRepository.Insert(task);
                return Ok(task);
            }
            catch (Exception ex)
            {
                _log.LogError($"[TaskController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates a Task object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/UpdateTask
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
        /// <param name="task">Task object that is going to be updated</param>
        /// <returns>Updated Task</returns>
        /// <response code="200">Returns the updated Task</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpPatch("UpdateTask")]
        public IActionResult Update([FromBody] Data.Models.Task task)
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

        /// <summary>
        /// Deletes a Task object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/DeleteTask
        /// </remarks>
        /// <param name="id">Task object that is going to be deleted</param>
        /// <returns>HTTP Response</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpDelete("DeleteTask")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
                taskRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _log.LogError($"[CustomerController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Select tasks or task by given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/task/ListTasks/1
        /// </remarks>
        /// <param name="id">Task id that is going to be search.</param>
        /// <returns>Found Tasks</returns>
        /// <response code="200">Returns Task List</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpGet("ListTasks")]
        public IActionResult Select(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = taskRepository.Select(id);
                return Ok(taskList);
            }
            catch (Exception ex)
            {
                _log.LogError($"[CustomerController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Select tasks or task by given id with joined objects
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/task/ListTasks/1
        /// </remarks>
        /// <param name="id">Task id that is going to be search.</param>
        /// <returns>Found Tasks</returns>
        /// <response code="200">Returns Task List</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpGet("ListTasksView")]
        public IActionResult SelectView(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new TaskRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = taskRepository.SelectView(id);
                return Ok(taskList);
            }
            catch (Exception ex)
            {
                _log.LogError($"[CustomerController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }
    }
}
