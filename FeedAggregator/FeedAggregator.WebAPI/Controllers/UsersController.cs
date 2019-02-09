using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.BLL.Interfaces;
using FeedAggregator.WebAPI.Helpers;
using FeedAggregator.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeedAggregator.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // POST api/users/authentication
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authenticate([FromBody, Bind("Username, Password")] UserModel user)
        {
            try
            {
                var userDto = _userService.Authenticate(user.Username, user.Password);

                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userDto.Id.ToString()),
                        new Claim(ClaimTypes.Role, userDto.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    userDto.Id,
                    userDto.Username,
                    userDto.FirstName,
                    userDto.LastName,
                    Token = tokenString
                });
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST api/users/registration
        [AllowAnonymous]
        [HttpPost("registration")]
        public IActionResult Register([FromBody, Bind("FirstName, LastName, Username, Password")] UserModel user)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                // add user role 
                userDto.Role = RoleType.User.ToString();
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

        // GET api/users/info
        [HttpGet("info")]
        public IActionResult Get()
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var userDto = _userService.GetById(currentUserId);

                return Ok(_mapper.Map<UserModel>(userDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        // PUT api/users/
        [HttpPut]
        public IActionResult Update([FromBody, Bind("FirstName, LastName, Username, Password")] UserModel userModel)
        {
            try
            {
                // map model to dto and set id
                var userDto = _mapper.Map<UserDto>(userModel);
                userDto.Id = int.Parse(User.Identity.Name);

                // save 
                _userService.Update(userDto);

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