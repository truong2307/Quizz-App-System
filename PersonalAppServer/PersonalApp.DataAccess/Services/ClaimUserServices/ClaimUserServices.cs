﻿using Microsoft.AspNetCore.Http;
using PersonalApp.DataAccess.Utility.BaseURI;
using System.Security.Claims;

namespace PersonalApp.DataAccess.Services.ClaimUserServices
{
    public class ClaimUserServices : IClaimUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimUserServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypeUser.TYPE_USERID)?.Value;
        }

        public string GetCurrentUserRole()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}