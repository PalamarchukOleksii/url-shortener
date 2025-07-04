using Moq;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Application.UseCases.Users.Command.SignUp;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Application.Tests.UseCases.Users.Commands.SignUp;

public class SignUpCommandHandlerTests
{
    private readonly SignUpCommandHandler _handler;
    private readonly Mock<IHasher> _hasherMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IRoleRepository> _roleRepositoryMock = new();
    private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock = new();

    public SignUpCommandHandlerTests()
    {
        _handler = new SignUpCommandHandler(
            _userRepositoryMock.Object,
            _hasherMock.Object,
            _roleRepositoryMock.Object,
            _userRoleRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Login_Is_Not_Unique()
    {
        
        _userRepositoryMock.Setup(r => r.ExistsByLoginAsync("test")).ReturnsAsync(true);

        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("test", "123456");

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsFailure);
        Assert.Equal("User.NotUniqueLogin", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Failed_To_Generate_Password_Hash()
    {
        
        _userRepositoryMock.Setup(r => r.ExistsByLoginAsync("test")).ReturnsAsync(false);
        _hasherMock.Setup(h => h.HashAsync("pass")).ReturnsAsync((string?)null);

        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("test", "pass");

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsFailure);
        Assert.Equal("Hasher.Failed", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_SignUp_Is_Valid()
    {
        
        _userRepositoryMock.Setup(r => r.ExistsByLoginAsync("newuser")).ReturnsAsync(false);
        _hasherMock.Setup(h => h.HashAsync("validpass")).ReturnsAsync("hashedpass");

        var userRole = new Role { Id = new RoleId(Guid.NewGuid()), Name = "User" };
        _roleRepositoryMock.Setup(r => r.GetByNameAsync("User")).ReturnsAsync(userRole);

        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
        _userRoleRepositoryMock.Setup(r => r.AddAsync(It.IsAny<UserRole>())).Returns(Task.CompletedTask);

        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("newuser", "validpass");

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsSuccess);
        _userRepositoryMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Login == "newuser")), Times.Once);
        _userRoleRepositoryMock.Verify(r => r.AddAsync(It.Is<UserRole>(ur => ur.RoleId == userRole.Id)), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Default_UserRole_Missing()
    {
        
        _userRepositoryMock.Setup(r => r.ExistsByLoginAsync("anotheruser")).ReturnsAsync(false);
        _hasherMock.Setup(h => h.HashAsync("validpass")).ReturnsAsync("hashedpass");
        _roleRepositoryMock.Setup(r => r.GetByNameAsync("User")).ReturnsAsync((Role?)null);

        var command = new Application.UseCases.Users.Command.SignUp.SignUpCommand("anotheruser", "validpass");

        
        var result = await _handler.Handle(command, CancellationToken.None);

        
        Assert.True(result.IsFailure);
        Assert.Equal("Role.MissingDefaultUserRole", result.Error.Code);
    }
}
