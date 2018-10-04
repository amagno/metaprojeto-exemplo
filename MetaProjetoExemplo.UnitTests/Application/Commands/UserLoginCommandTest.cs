using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MetaProjetoExemplo.Application.Commands.Common;
using MetaProjetoExemplo.Application.Exceptions;
using MetaProjetoExemplo.Application.Services.Common;
using MetaProjetoExemplo.Domain.Events;
using Moq;
using Xunit;

namespace MetaProjetoExemplo.UnitTests.Application.Commands
{
    public class UserLoginCommandTest
    {
      /// <summary>
      /// Testa commando de login quando dados estiverem corretos,
      /// verifica se eventos de tentativa de login e sucesso de login foram publicados
      /// </summary>
      [Fact]
      public async Task Test_return_auth_data_and_publish_events_with_login_success()
      {
        // comand data
        var command = new UserLoginCommand("teste@test.com", "123", "ip_info_test");
        // mocks
        var mediatorMock = new Mock<IMediator>();
        var authServiceMock = new Mock<IAuthService>();
        // login data
        var authData = new AuthData {
          Token = "testing token",
          UserIdentifier = Guid.NewGuid()
        };
        // auth service mock
        authServiceMock
          .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
          .ReturnsAsync(authData);

        // handle command
        var handler = new UserLoginCommandHandler(mediatorMock.Object, authServiceMock.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());
        // verifica se eventos foram publicados
        // tentativa de login apenas um evento
        mediatorMock
          .Verify(m => m.Publish(
            It.IsAny<LoginAttemptActionEvent>(), 
            It.IsAny<CancellationToken>()
          ), Times.Once());
        // sucesso de login apenas um evento
        mediatorMock
          .Verify(m => m.Publish(
            It.IsAny<LoginSuccessActionEvent>(), 
            It.IsAny<CancellationToken>()
          ), Times.Once());
        // verifica resultado do command handler
        Assert.Equal(authData, result);
      }
      /// <summary>
      /// Testa se a exception do tipo InvalidRequestException foi lançanda
      /// verifica se eventos de tentativa de login e falha de login foram publicados 
      /// </summary>
      [Fact]
      public async Task Test_throw_invalid_request_exception_with_fail_login()
      {
        // comand data
        var command = new UserLoginCommand("teste@test.com", "123", "ip_info_test");
        // mocks
        var mediatorMock = new Mock<IMediator>();
        var authServiceMock = new Mock<IAuthService>();
    
        // auth service mock
        authServiceMock
          .Setup(a => a.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
          .ThrowsAsync(new InvalidRequestException());

        // handle command
        var handler = new UserLoginCommandHandler(mediatorMock.Object, authServiceMock.Object);
        // assert exception
        await Assert.ThrowsAsync<InvalidRequestException>(() => 
          handler.Handle(command, It.IsAny<CancellationToken>()
        ));
        // verifica se enventos foram publicados
        // tentativa de login apenas um evento
        mediatorMock
          .Verify(m => m.Publish(
            It.IsAny<LoginAttemptActionEvent>(), 
            It.IsAny<CancellationToken>()
          ), Times.Once());
        // sucesso de login apenas um evento
        mediatorMock
          .Verify(m => m.Publish(
            It.IsAny<LoginFailActionEvent>(), 
            It.IsAny<CancellationToken>()
          ), Times.Once());
      }
    }
}