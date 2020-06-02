using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Theatre.Web.Core.Helpers.Commons;
using Theatre.Web.Core.Helpers.Commons.Filters.Implementations;
using Theatre.Web.Core.Helpers.Network.Models;
using Theatre.Web.Core.Models.Entities;
using Theatre.Web.Core.Services.Interfaces;

namespace Theatre.Web.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService m_userService;
        private readonly ILogger<UserController> m_logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            m_userService = userService;
            m_logger = logger;
        }

        [HttpPost("get")]
        public async Task<Response> GetUsersAsync([FromBody] SearchRequest<User, UserFilter> request)
        {
            return new Response
            {
                IsSuccesfull = true,
                Message = JsonConvert.SerializeObject(await m_userService.SearchUserAsync(request.Pagination, request.Filtering))
            };
        }

        [HttpPost("Login")]
        public async Task<Response> LoginUsersAsync([FromBody] User user)
        {
            var (_, result) = await m_userService.SearchUserAsync(new Pagination(), new UserFilter
            {
                SearchTerm = user.Username
            });
            return new Response
            {
                IsSuccesfull = result.First().Password.Equals(user.Password)
            };
        }

        [HttpPost]
        public async Task<Response> AddAsync([FromBody] User user)
        {
            await m_userService.CreateUserAsync(user);
            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpPut]
        public async Task<Response> UpdateAsync([FromBody] IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                await m_userService.UpdateUserAsync(user);
            }

            return new Response
            {
                IsSuccesfull = true
            };
        }

        [HttpDelete]
        public async Task<Response> DeleteAsync([FromBody] Guid id)
        {
            await m_userService.DeleteUserAsync(id);
            return new Response
            {
                IsSuccesfull = true
            };
        }
    }
}