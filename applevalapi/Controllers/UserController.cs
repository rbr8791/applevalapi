using Microsoft.AspNetCore.Mvc;
using System.Linq;
using applevalApi.DAL;
using applevalApi.DTO.Helpers;
using applevalApi.DTO;
using applevalApi.DAL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using applevalApi.Entities;
using applevalApi.DTO.MapperObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using applevalApi.DTO.MapperObjects;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Configuration;
using Microsoft.AspNetCore.Authorization;
using applevalApi.Helpers;

namespace applevalApi.Controllers
{

    /*
     * 
     * 
     * 
    [ApiVersion( "1.0" )]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class HelloWorldController : Controller {
        public string Get() => "Hello world!";
    }
 
    [ApiVersion( "2.0" )]
    [ApiVersion( "3.0" )]
    [Route( "api/v{version:apiVersion}/helloworld" )]
    public class HelloWorld2Controller : Controller {
        [HttpGet]
        public string Get() => "Hello world v2!";
 
        [HttpGet, MapToApiVersion( "3.0" )]
        public string GetV3() => "Hello world v3!";
    }
    */
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
       
        public UserController(IUserService userService,
            IMapper mapper, IOptions<AppSettings> settings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = settings.Value;
          

        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the user in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="applevalApi.DTO.ViewModels.UserViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var user = _userService.FindByOrdinal(id);
            if (user == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(UserViewModelHelper.ConvertToViewModel(user));
        }

        //[HttpGet("Get/{id}")]
        //[MapToApiVersion("2")]
        //public JsonResult GetByOrdinal2(int id)
        //{
        //    var user = _userService.FindByOrdinal(id);
        //    if (user == null)
        //    {
        //        return ErrorResponse("Not found");
        //    }

        //    return SingleResult(UserViewModelHelper.ConvertToViewModel(user));
        //}


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });


        } // End Authenticate


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                _userService.Create(user, userDto.Password);
                return Ok(new
                {
                    message = "Ok"
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }





    } // End Class
} // End Namespace
                  