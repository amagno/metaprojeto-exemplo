using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Application.ViewModels;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Security;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Services
{
  public class AuthServiceTest
  {
    [Fact]
    public async Task Test_valid_login_should_return_valid_token()
    {
      // SETUP 
      var tokenValue = "token";
      var email = "teste@teste.com";
      var password = "123";
      var name = "teste";
      var user = new User(name, email, password);

      var userRepositoryMock = new Mock<IUserRepository>();

      userRepositoryMock
        .Setup(u => u.GetByEmailAsync(It.IsAny<string>()))
        .ReturnsAsync(user);

      var actionLogServiceMock = new Mock<IActionLogService>();
      var jwtMock = new Mock<IJwtAuth>();

      jwtMock.Setup(j => j.CreateToken(It.IsAny<IEnumerable<Claim>>(), 600)).Returns(tokenValue);

      // EXECUTION
      var service = new AuthService(userRepositoryMock.Object, actionLogServiceMock.Object, jwtMock.Object);
      var token = await service.LoginAsync(new Login {
        Email = email,
        Password = password
      });
      // ASSERTS
      Assert.Equal(tokenValue, token);
      // log de tentativa de login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginAttempt, It.IsAny<string>()), Times.Once);
      // log de sucesso no login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginSuccess, It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public async Task Test_invalid_email_login_should_throw_exception()
    {
      // SETUP
      var userRepositoryMock = new Mock<IUserRepository>();
      var actionLogServiceMock = new Mock<IActionLogService>();
      var jwtMock = new Mock<IJwtAuth>();

      var service = new AuthService(userRepositoryMock.Object, actionLogServiceMock.Object, jwtMock.Object);

      // EXECUTION
      await Assert.ThrowsAsync<InvalidUserEmailException>(async () => {
        await service.LoginAsync(new Login {
          Email = "teste@invalido.com",
          Password = "123"
        });
      });
      // ASSERTS
      // log de tentativa de login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginAttempt, It.IsAny<string>()), Times.Once);
      // log de falha de login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginFail, It.IsAny<string>()), Times.Once);
    }
    [Fact]
    public async Task Test_invalid_password_login_should_throw_exception()
    {

      var email = "teste@teste.com";
      var password = "123";
      var name = "teste";
      var user = new User(name, email, password);

      // SETUP
      var userRepositoryMock = new Mock<IUserRepository>();

      userRepositoryMock.Setup(u => u.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

      var actionLogServiceMock = new Mock<IActionLogService>();
      var jwtMock = new Mock<IJwtAuth>();

      var service = new AuthService(userRepositoryMock.Object, actionLogServiceMock.Object, jwtMock.Object);

      // EXECUTION
      await Assert.ThrowsAsync<InvalidUserPasswordException>(async () => {
        await service.LoginAsync(new Login {
          Email = email,
          Password = "wrong_password"
        });
      });
      // ASSERTS
      // log de tentativa de login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginAttempt, It.IsAny<string>()), Times.Once);
      // log de falha de login
      actionLogServiceMock.Verify(a => a.RegisterLogAsync(ActionLogType.UserLoginFail, It.IsAny<string>()), Times.Once);
    }
  }
}