using Microsoft.AspNetCore.Identity;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Models.DTOs;
using System;

namespace NUnit.Test.Application.Repository
{
    public interface IUserRepository
    {
        public Task<IdentityResult> RegisterUser(User user, string Password);
        public Task<User> FindByEmail(string email);

        public Task<Boolean> CheckPasswordAsync(User userData, string password);
        public Task<UserDTO> GetUserByGuid(string guid);

        public Task<IEnumerable<UserDTO>> GetAllUser();
        //public Task<IEnumerable<string>> UserRoles(User user);

        // public Task<IdentityResult> CreateAdmin(User userData, string password);
    }
}
