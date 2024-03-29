﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalApp.DataAccess.AuthenticationService;
using PersonalApp.DataAccess.Constants;
using PersonalApp.Models.Dto;
using PersonalApp.Models.Identity;

namespace PersonalApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public UserController(IMapper mapper, UserManager<ApiUser> userManager, IAuthManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Register([FromBody] UserDto userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userToDb = _mapper.Map<ApiUser>(userRequest);
            var userResponse = await _userManager.CreateAsync(userToDb, userRequest.Password);

            if (!userResponse.Succeeded)
            {
                foreach (var error in userResponse.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(userToDb, Identity.Role.USER_ROLE);

            return Accepted();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool loginUserSucceeded = await _authManager.ValidateUser(userRequest);
            if (!loginUserSucceeded) return BadRequest("Sai mật khẩu");

            return Accepted(new { token = await _authManager.CreateToken() });
        }
    }
}
