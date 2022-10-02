﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMate.Business.Repositories;

namespace TransferMate.Web.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommentController> _log;

        public CommentController(IConfiguration configuration, ILogger<CommentController> log)
        {
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// Creates a Comment object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/CreateTask
        ///
        ///      {
        ///        "CreatedDate": "2022-10-01 19:03:37.017",
        ///        "CommentContent": "Sample Comment",
        ///        "CommentType": 1,
        ///        "ReminderDate": 1,
        ///        "Task": 1
        ///      }
        ///
        /// </remarks>
        /// <param name="comment">Comment object that is going to be created</param>
        /// <returns>Created Comment</returns>
        /// <response code="200">Returns the inserted Comment</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpPost("CreateComment")]
        public IActionResult Create([FromBody] Data.Models.Comment comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new CommentRepository(_configuration.GetConnectionString("DefaultConnection"));
                taskRepository.Insert(comment);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _log.LogError($"[TaskController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates a Comment object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/UpdateComment
        ///
        ///     {
        ///        "CreatedDate": "2022-10-01 19:03:37.017",
        ///        "CommentContent": "Sample Comment",
        ///        "CommentType": 1,
        ///        "ReminderDate": 1,
        ///        "Task": 1
        ///      }
        ///
        /// </remarks>
        /// <param name="comment">Comment object that is going to be updated</param>
        /// <returns>Updated Comment</returns>
        /// <response code="200">Returns the updated Comment</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpPatch("UpdateComment")]
        public IActionResult Update([FromBody] Data.Models.Comment comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new CommentRepository(_configuration.GetConnectionString("DefaultConnection"));
                taskRepository.Update(comment);
                return Ok(comment);
            }
            catch (Exception ex)
            {
                _log.LogError($"[CustomerController.Create] Someting went wrong {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes a Comment object
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Post /api/task/DeleteComment
        /// </remarks>
        /// <param name="id">Comment object that is going to be deleted</param>
        /// <returns>HTTP Response</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpDelete("DeleteComment")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new CommentRepository(_configuration.GetConnectionString("DefaultConnection"));
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
        /// Select comments or comment by given id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///         GET /api/task/ListComments/1
        /// </remarks>
        /// <param name="id">Comment id that is going to be search.</param>
        /// <returns>Found Comments</returns>
        /// <response code="200">Returns Comment List</response>
        /// <response code="400">If Task validation is failed</response>
        [HttpGet("ListComments")]
        public async Task<IActionResult> Select(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _log.LogInformation("[TaskController.Create] Processing Create request");
                var taskRepository = new CommentRepository(_configuration.GetConnectionString("DefaultConnection"));
                var taskList = taskRepository.Select(id);
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
