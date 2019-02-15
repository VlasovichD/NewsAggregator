using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.BLL.Interfaces;
using FeedAggregator.WebAPI.Helpers;
using FeedAggregator.WebAPI.Infrastructure;
using FeedAggregator.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace FeedAggregator.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AdminController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // POST api/admin/users/regadmin
        [HttpPost("users/regadmin")]
        public IActionResult RegisterAdmin([FromBody, Bind("FirstName, LastName, Username, Password")] UserModel user)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                // add admin role
                userDto.Role = RoleType.Admin.ToString();
                // save 
                var newUserDto = _userService.Create(userDto);

                return Ok(_mapper.Map<UserModel>(newUserDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/admin/users
        [HttpGet("users")]
        public IActionResult GetAll()
        {
            try
            {
                var userDtos = _userService.GetAll();

                return Ok(_mapper.Map<List<UserModel>>(userDtos));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/admin/users/{id}
        [HttpGet("users/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var userDto = _userService.GetById(id);

                return Ok(_mapper.Map<UserModel>(userDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/admin/users/{id}
        [HttpDelete("users/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}