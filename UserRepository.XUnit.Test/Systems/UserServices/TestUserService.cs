using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Test.Application.Controllers;
using NUnit.Test.Application.Domain;
using NUnit.Test.Application.Models.DTOs;
using NUnit.Test.Application.Repository;
using NUnit.Test.Application.Services;
using Xunit;

namespace UserRepository.XUnit.Test.Systems.UserServices
{
    public class TestUserService
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _mockRepository = new Mock<IUserRepository>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<UserManager<User>> _mockUserManager = new Mock<UserManager<User>>();
        private readonly Mock<ILogger<UserService>> _mockLogger = new Mock<ILogger<UserService>>();

        public TestUserService()
        {
            _sut = new UserService(_mockRepository.Object, _mockMapper.Object,_mockLogger.Object);
        }
        [Fact]
        public async Task RegisterUser_ValidInput_Success()
        {
            // Arrange
            var userSignUpDTO = new UserSIgnUpDTO
            {
                Email = "test1@gmail.com",
                Name = "Test1",
                PhoneNumber = "9608140772",
                Password = "Test@123"
            };

            var expectedUser = new User
            {
                UserName = userSignUpDTO.Email,
                Email = userSignUpDTO.Email,
                PhoneNumber = userSignUpDTO.PhoneNumber,
                Name = userSignUpDTO.Name,
            };
            _mockMapper.Setup(m => m.Map<UserSIgnUpDTO, User>(userSignUpDTO)).Returns(expectedUser);
            _mockRepository.Setup(repo => repo.RegisterUser(expectedUser, userSignUpDTO.Password)).ReturnsAsync(IdentityResult.Success);
            // Act
            var result = await _sut.RegisterUser(userSignUpDTO);
            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task Get_All_User_RturnAllUserStatusCode200()
        {
            // Arrange
            var expectedUsers = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = "b05e44a3-75a7-4f29-8bde-f9da67d8818f",
                    Name = "test",
                    Email = "test@gmail.com",
                    NormalizedEmail = "TEST@GMAIL.COM",
                    PhoneNumber = "9608140776"
                }
            };
            _mockRepository.Setup(repo => repo.GetAllUser()).ReturnsAsync(expectedUsers);
            //Act
            var result = await _sut.GetAllUser();
            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedUsers.First().Id, result.First().Id);
            Assert.Equal(expectedUsers.First().Name, result.First().Name);
            Assert.Equal(expectedUsers.First().Email, result.First().Email);
            Assert.Equal(expectedUsers.First().NormalizedEmail, result.First().NormalizedEmail);
            Assert.Equal(expectedUsers.First().PhoneNumber, result.First().PhoneNumber);
        }
        [Fact]
        public async Task GetUserById_ReturnsUserDTO()
        {
            // Arrange
            var userId = "b05e44a3-75a7-4f29-8bde-f9da67d8818f";
            var expectedUser = new UserDTO
            {
                Id = userId,
                Name = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                PhoneNumber = "9608140776"
            };
            _mockRepository.Setup(repo => repo.GetUserByGuid(userId)).ReturnsAsync(expectedUser);
            // Act
            var result = await _sut.GetUserByGuid(userId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Name, result.Name);
            Assert.Equal(expectedUser.Email, result.Email);
            Assert.Equal(expectedUser.NormalizedEmail, result.NormalizedEmail);
            Assert.Equal(expectedUser.PhoneNumber, result.PhoneNumber);
        }
    }
}
