using Microsoft.AspNetCore.Identity;
using NUnit.Test.Application.Models.DTOs;
using System;

namespace NUnit.Test.Application.Services
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterUser(UserSIgnUpDTO userSignUpDTO);
        public Task<UserDTO> FindByEmailAsync(string email);

        public Task<Boolean> CheckPasswordAsync(UserDTO user, string password);
        public Task<UserDTO> GetUserByGuid(string guid);

        public Task<IEnumerable<UserDTO>> GetAllUser();
       // public Task<IEnumerable<string>> UserRoles(UserDTO user);
       // public Task<IdentityResult> CreateAdmin(UserSignUpDTO userSignUpDTO);
    }
}
