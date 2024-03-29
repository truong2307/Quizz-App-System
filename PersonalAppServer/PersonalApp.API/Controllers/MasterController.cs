﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalApp.DataAccess.Constants;
using PersonalApp.DataAccess.Services.MasterDataServices;
using PersonalApp.Models.Dto;

namespace PersonalApp.Controllers
{
    [Route("api/master-data")]
    [ApiController]
    [Authorize(Roles = Identity.Role.ADMIN_ROLE)]
    public class MasterController : Controller
    {
        private readonly IMasterDataServices _masterDataServices;

        public MasterController(IMasterDataServices masterDataServices)
        {
            _masterDataServices = masterDataServices;
        }

        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _masterDataServices.GetRoles();
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpGet("get-quizz-topics/{pageIndex:int}/{pageSize:int}")]
        public async Task<IActionResult> GetQuizzTopics(int pageIndex, int pageSize)
        {
            var result = await _masterDataServices.GetQuizzTopics(pageIndex, pageSize);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpGet("get-all-quizz-topics")]
        public async Task<IActionResult> GetQuizzTopics()
        {
            var result = await _masterDataServices.GetQuizzTopics();
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpPost("create-quizz-topics")]
        public async Task<IActionResult> CreateQuizzTopics(QuizzTopicCreateDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
            var result = await _masterDataServices.CreateQuizzTopic(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpPut("update-quizz-topics")]
        public async Task<IActionResult> UpdateQuizzTopics(QuizzTopicDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
            var result = await _masterDataServices.UpdateQuizzTopic(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }

        [HttpDelete("delete-quizz-topics/{id:int}")]
        public async Task<IActionResult> DeleteQuizzTopics(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.ToString());
            var result = await _masterDataServices.DeleteQuizzTopic(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessages);
        }
    }
}
