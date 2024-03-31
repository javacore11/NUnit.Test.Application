using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Test.Application.DataDb;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Models.DTOs;

namespace NUnit.Test.Application.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;
            InitializeRoles().GetAwaiter().GetResult();
        }
        //AsigningRoles
        private async Task InitializeRoles()
        {
            string[] roleNames = { "User", "Admin" };

            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "User");
            return res;
        }
        public async Task<User> FindByEmail(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<Boolean> CheckPasswordAsync(User userData, string password)
        {
            return await _userManager.CheckPasswordAsync(userData, password);
        }
        public async Task<UserDTO> GetUserByGuid(string guid)
        {
            var res = await _userManager.FindByIdAsync(guid);
            return _mapper.Map<User,UserDTO>(res);
        }
        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            var user = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<User>,IEnumerable<UserDTO>>(user);

        }

    }
}
