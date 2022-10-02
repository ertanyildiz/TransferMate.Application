using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StatusController> _log;

        public StatusController(IConfiguration configuration, ILogger<StatusController> log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Select status or status by given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/task/ListStatus/1
        /// </remarks>
        /// <param name="id">Status id that is going to be search.</param>
        /// <returns>Found Status</returns>
        /// <response code="200">Returns Status List</response>
        /// <response code="400">If Status validation is failed</response>
        [HttpGet("ListStatus")]
        public IActionResult Select(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Select request");
                var statusRepository = new StatusRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = statusRepository.Select(id);
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
