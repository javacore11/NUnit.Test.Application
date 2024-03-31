using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Test.Application.Models.DTOs;
using NUnit.Test.Application.Services;
using Serilog;

namespace NUnit.Test.Application.Controllers
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDTO;
        private readonly ILogger<UserController> _logger;
       // private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
            _logger = logger;
            //_jwtTokenGenerator = jwtTokenGenerator;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserSIgnUpDTO userSignUpDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _userService.RegisterUser(userSignUpDTO);
            if (!res.Succeeded)
            {
                return Ok("User Already Exist!");
            }
            Log.Information("Users retrieved: {@Users}", res);
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "User Created Successfully!";
           // _responseDTO.Token = "";
            _responseDTO.User = res;
            return Ok(_responseDTO);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserSignInDTO userSignInDTO)
        {
            var user = await _userService.FindByEmailAsync(userSignInDTO.Email);
            if (user == null || !await _userService.CheckPasswordAsync(user, userSignInDTO.Password))
            {
                _logger.LogInformation("User is not Present!");
                return NotFound("User not Found!");
            }
            //IEnumerable<string> roles = await _userService.UserRoles(user);
           // var token = _jwtTokenGenerator.GenerateToken(user, roles);
            //Token
            //Logger Information
            _logger.LogInformation($"{nameof(Login)}");
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Login Successfully!";
            //_responseDTO.Token = token;
            _responseDTO.User = user;
            return Ok(_responseDTO);
        }
        [HttpGet]
        [Route("get-user/{guid}")]
        public async Task<IActionResult>GetUserByGuid(string guid)
        {
            var res = await _userService.GetUserByGuid(guid);
            if (res==null)
            {
                return BadRequest("User not Found!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "User Get SuccessFully!";
            _responseDTO.User = res;
            return Ok(_responseDTO);
        }
        [HttpGet]
        [Route("get-all-user")]
        public async Task<IActionResult> GetAllUser()
        {
            var res = await _userService.GetAllUser();
            if (res == null)
            {
                return BadRequest("User not Found!");
            }
            //_logger.LogInformation(res.ToString());
            //Log.Information(res.ToString());
            Log.Information("Users retrieved: {@Users}", res);
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "User Get SuccessFully!";
            _responseDTO.User = res;
            return Ok(_responseDTO);
        }

       
    }
}
 