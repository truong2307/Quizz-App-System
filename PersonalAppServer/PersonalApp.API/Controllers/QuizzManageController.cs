﻿using Microsoft.AspNetCore.Mvc;
using PersonalApp.API.Helper;
using PersonalApp.DataAccess.Constants;
using PersonalApp.DataAccess.Services.QuizzAdminServices;
using PersonalApp.Models.Dto;

namespace PersonalApp.API.Controllers
{
    [Route("api/quizz-manage")]
    [ApiController]
    [AuthorizeRoles(Identity.Role.ADMIN_ROLE, Identity.Role.MANAGER_ROLE)]
    public class QuizzManageController : ControllerBase
    {
        private readonly IQuizzAdminServices _quizzAdminServices;
        public QuizzManageController(IQuizzAdminServices quizzAdminServices)
        {
            _quizzAdminServices = quizzAdminServices;
        }

        [HttpPost("create-quizz")]
        public async Task<IActionResult> Post([FromForm] QuizzCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
            var result = await _quizzAdminServices.CreateQuizz(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpGet("get-all-quizz/{pageIndex:int}/{pageSize:int}")]
        public async Task<IActionResult> GetAllQuizz(int pageIndex, int pageSize)
        {
            var result = await _quizzAdminServices.GetQuizzs(pageIndex, pageSize);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpGet("get-quizz-by-id/{id:int}")]
        public async Task<IActionResult> GetAllQuizz(int id)
        {
            var result = await _quizzAdminServices.GetQuizzById(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpPut("update-quizz")]
        public async Task<IActionResult> UpdateQuizz([FromForm] QuizzUpdateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
            var result = await _quizzAdminServices.UpdateQuizz(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpDelete("delete-quizz/{quizzId:int}")]
        public async Task<IActionResult> DeleteQuizz(int quizzId)
        {
            var result = await _quizzAdminServices.DeleteQuizz(quizzId);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }
    }
}
