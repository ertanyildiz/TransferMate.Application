using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _log;

        public UserController(IConfiguration configuration, ILogger<UserController> log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Select type or type by given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/type/ListTypes/1
        /// </remarks>
        /// <param name="id">Type id that is going to be search.</param>
        /// <returns>Found Types</returns>
        /// <response code="200">Returns Type List</response>
        /// <response code="400">If Type validation is failed</response>
        [HttpGet("ListUsers")]
        public IActionResult Select(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Select request");
                var userRepository = new UserRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = userRepository.Select(id);
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
