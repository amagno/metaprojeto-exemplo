using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Common;
using MetaProjetoExemplo.Security;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Services
{
  public class AuthServiceTest
  {
    /// <summary>
    /// Testa serviço de login com dados validos, deve retornar token valido
    /// </summary>
    [Fact]
    public async Task Test_valid_login_should_return_fake_token()
    {
      // fake token
      var tokenValue = "token";
      // cria usuário
      var email = "teste@teste.com";
      var password = "123";
      var name = "teste";
      var user = new User(name, email, password);

      // mock do repositorio retorna usuario criado
      var userRepositoryMock = new Mock<IUserRepository>();
      userRepositoryMock
        .Setup(u => u.GetByEmailAsync(It.IsAny<string>()))
        .ReturnsAsync(user);
      // mock do serviço de criação de token retorna token falso
      var jwtMock = new Mock<IJwtAuth>();
      jwtMock.Setup(j => j.CreateToken(It.IsAny<IEnumerable<Claim>>(), 600)).Returns(tokenValue);

      // EXECUTION
      var service = new AuthService(userRepositoryMock.Object, jwtMock.Object);
      var result = await service.LoginAsync(email, password);
      // ASSERTS
      Assert.Equal(tokenValue, result.Token);
    }
    /// <summary>
    /// Testa serviço de login quando usuário não existe
    /// </summary>
    [Fact]
    public async Task Test_invalid_email_login_should_throw_exception()
    {
      // SETUP
      var userRepositoryMock = new Mock<IUserRepository>();
      var jwtMock = new Mock<IJwtAuth>();

      var service = new AuthService(userRepositoryMock.Object, jwtMock.Object);

      // quando usuário não existe
      await Assert.ThrowsAsync<InvalidUserEmailException>(async () => {
        await service.LoginAsync("teste@invalido.com", "123");
      });
    }
    /// <summary>
    /// Testa serviço de login quando usuário existe e senha está incorreta
    /// </summary>
    [Fact]
    public async Task Test_invalid_password_login_should_throw_exception()
    {
      // cria usuário
      var email = "teste@teste.com";
      var password = "123";
      var name = "teste";
      var user = new User(name, email, password);

      // mock do repositorio retorna usuario criado
      var userRepositoryMock = new Mock<IUserRepository>();
      userRepositoryMock.Setup(u => u.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

      var jwtMock = new Mock<IJwtAuth>();
      var service = new AuthService(userRepositoryMock.Object, jwtMock.Object);

      // quando senha for incorreta
      await Assert.ThrowsAsync<InvalidUserPasswordException>(async () => {
        await service.LoginAsync(email, "wrong_password");
      });
    }
  }
}