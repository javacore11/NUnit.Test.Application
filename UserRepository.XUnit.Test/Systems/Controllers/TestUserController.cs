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

namespace UserRepository.XUnit.Test.Systems.Controllers
{
    public class TestUserController
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _mockUserService = new Mock<IUserService>();
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ILogger<UserController>> _mockLogger = new Mock<ILogger<UserController>>();
        private readonly Mock<UserManager<User>> _mockUserManager = new Mock<UserManager<User>>();
        public TestUserController()
        {
            _controller = new UserController(_mockUserService.Object,_mockMapper.Object,_mockLogger.Object);
        }
        [Fact]
        public async Task User_Login_WithUserNameAndPassword_ValidCredentials()
        {
            // Arrange
            var userSignInDTO = new UserSignInDTO
            {
                Email = "test@gmail.com",
                Password = "Test@123"
            };
            var userDTO = new UserDTO
            {
                Id = "b05e44a3-75a7-4f29-8bde-f9da67d8818f",
                Name = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                PhoneNumber = "9608140776"
            };
            _mockUserService.Setup(repo => repo.FindByEmailAsync(userSignInDTO.Email)).ReturnsAsync(userDTO);
            _mockUserService.Setup(repo => repo.CheckPasswordAsync(userDTO, userSignInDTO.Password)).ReturnsAsync(true);
            // Act
            var result = await _controller.Login(userSignInDTO);
            // Assert
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
            var responseDTO = Assert.IsType<ResponseDTO>(okResult.Value);
            Assert.True(responseDTO.IsSuccess);
            Assert.Equal("Login Successfully!", responseDTO.Message);
            Assert.Equal(userDTO, responseDTO.User);
        }

    }
}