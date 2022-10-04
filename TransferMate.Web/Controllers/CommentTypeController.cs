using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    public class CommentTypeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommentTypeController> _log;

        public CommentTypeController(IConfiguration configuration, ILogger<CommentTypeController> log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Select comment types or comment types by given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/task/ListStatus/1
        /// </remarks>
        /// <param name="id">comment type id that is going to be search.</param>
        /// <returns>Found comment types</returns>
        /// <response code="200">Returns comment type List</response>
        /// <response code="400">If comment type validation is failed</response>
        [HttpGet("ListCommentTypes")]
        public IActionResult Select(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Select request");
                var commentType = new CommentTypeRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = commentType.Select(id);
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
