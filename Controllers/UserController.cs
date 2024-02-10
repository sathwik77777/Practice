using AutoMapper;
using Practice_test1.Entities;
using Practice_test1.DTO;
using Practice_test1.Services;
using Practice_test1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using log4net;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Practice_test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private ILogger<UserController> logger;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpGet, Route("GetAllUsers")]
        //[Authorize(IsAdmin = True)]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
                return Ok(users);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Register")]
        [AllowAnonymous] 
        public IActionResult AddUser(User user)
        {
            try
            {
                User _user = _mapper.Map<User>(user);
                userService.CreateUser(_user);
                return StatusCode(200, _user);


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetUserById")]
        //[Authorize(IsAdmin = True)]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                User user = userService.GetUser(userId);
                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                if (user != null)
                {
                    return Ok(userDTO);
                }
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditUser")]
        //[Authorize(IsAdmin=False)]
        public IActionResult EditUser(User user)
        {
            try
            {
                User _user = _mapper.Map<User>(user);
                userService.EditUser(_user);
                return StatusCode(200, user);


            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteUser/{userId}")]
        //[Authorize(IsAdmin=True)]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                userService.DeleteUser(userId);
                return StatusCode(200, new JsonResult($"User with Id {userId} is Deleted"));

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Validate")]
        //[AllowAnonymous]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.UserName, login.Password);
                AuthResponse authReponse = new AuthResponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.UserName = user.UserName;
                    authReponse.Password = user.Password;
                    authReponse.RoleName =  user.RoleName;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name ,user.UserName),
                        new Claim(ClaimTypes.Role, user.RoleName ),
                        new Claim(ClaimTypes.Email,user.Email),
             });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
