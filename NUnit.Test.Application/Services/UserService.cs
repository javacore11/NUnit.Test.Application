using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Models.DTOs;
using NUnit.Test.Application.Repository;

namespace NUnit.Test.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IdentityResult> RegisterUser(UserSIgnUpDTO userSignUpDTO)
        {
            if (userSignUpDTO.Email == "")
            {
                //_logger.LogInformation("Sorry Email is empty");
                _logger.LogError("Sorry!");
            }
            var userData = _mapper.Map<UserSIgnUpDTO, User>(userSignUpDTO);
            userData.UserName = userData.Email;
            var res = await _userRepository.RegisterUser(userData, userSignUpDTO.Password);
            return res;
        }
        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            return _mapper.Map<User, UserDTO>(user);
        }
        public async Task<Boolean> CheckPasswordAsync(UserDTO user, string password)
        {
            User userData = await _userRepository.FindByEmail(user.Email);
            return await _userRepository.CheckPasswordAsync(userData, password);
        }
        public async Task<UserDTO> GetUserByGuid(string guid)
        {
            var res = await _userRepository.GetUserByGuid(guid);
            return res;
        }
        public async  Task<IEnumerable<UserDTO>> GetAllUser()
        {
            return await _userRepository.GetAllUser();
        }
        //public async Task<IEnumerable<string>> UserRoles(UserDTO userDTO)
        //{
        //    User user = await _userRepository.FindByEmail(userDTO.Email);
        //    return await _userRepository.UserRoles(user);
        //}
        //Admin Creation
        //public async Task<IdentityResult> CreateAdmin(UserSignUpDTO userSignUpDTO)
        //{
        //    var userData = _mapper.Map<UserSignUpDTO, User>(userSignUpDTO);
        //    userData.UserName = userData.Email;
        //    var res = await _userRepository.CreateAdmin(userData, userSignUpDTO.Password);
        //    return res;
        //}
    }
}
