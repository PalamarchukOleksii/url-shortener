using Moq;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Application.UseCases.Users.Command.SignIn;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Tests.UseCases.Users.Commands;

public class SignInCommandHandlerTests
{
    private readonly SignInCommandHandler _handler;
    private readonly Mock<IHasher> _hasherMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock = new();

    public SignInCommandHandlerTests()
    {
        _handler = new SignInCommandHandler(
            _userRepositoryMock.Object,
            _hasherMock.Object,
            _userRoleRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Return_Success_When_Credentials_Are_Valid()
    {
        // Arrange
        var userId = new UserId(Guid.NewGuid());
        var user = new User { Id = userId, Login = "test", HashedPassword = "hashedpass" };

        _userRepositoryMock.Setup(r => r.GetByLoginAsync("test")).ReturnsAsync(user);
        _hasherMock.Setup(h => h.VerifyAsync("123456", "hashedpass")).ReturnsAsync(true);
        _userRoleRepositoryMock.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(new List<UserRole>
        {
            new() { Role = new Role { Name = "Admin" } }
        });

        var command = new SignInCommand("test", "123456");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("test", result.Value.Login);
        Assert.Contains("Admin", result.Value.Roles.Select(r => r.Name));
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_User_Not_Found()
    {
        _userRepositoryMock.Setup(r => r.GetByLoginAsync("notfound")).ReturnsAsync(default(User));

        var command = new SignInCommand("notfound", "123456");

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("User.NotFound", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Password_Is_Incorrect()
    {
        var user = new User { Id = new UserId(Guid.NewGuid()), Login = "test", HashedPassword = "hashedpass" };

        _userRepositoryMock.Setup(r => r.GetByLoginAsync("test")).ReturnsAsync(user);
        _hasherMock.Setup(h => h.VerifyAsync("wrong", "hashedpass")).ReturnsAsync(false);

        var command = new SignInCommand("test", "wrong");

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("User.IncorrectPassword", result.Error.Code);
    }
}