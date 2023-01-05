using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Controllers.Customer;
using ShradhaBook_API.Services.EmailService;
using ShradhaBook_API.Services.UserService;
using ShradhaBook_ClassLibrary.Dto;
using ShradhaBook_ClassLibrary.Entities;

namespace ShradhaBook_UnitTest.Controllers;

public class UserControllerTests
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserControllerTests()
    {
        _userService = A.Fake<IUserService>();
        _emailService = A.Fake<IEmailService>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public async void UserController_GetAllUsers_ReturnOk()
    {
        // Arrange
        var user = A.Fake<User>();
        var userDto = A.Fake<UserDto>();
        A.CallTo(() => _mapper.Map<UserDto>(user)).Returns(userDto);
        var controller = new UserController(_userService, _emailService, _mapper);

        // Act
        var result = await controller.GetSingleUser(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
}