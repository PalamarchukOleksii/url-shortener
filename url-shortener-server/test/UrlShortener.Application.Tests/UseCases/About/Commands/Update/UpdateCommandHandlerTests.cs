

using Moq;
using UrlShortener.Application.UseCases.Abouts.Commands.Update;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Models.RoleModel;
using UrlShortener.Domain.Models.UserModel;
using UrlShortener.Domain.Models.UserRoleModel;

namespace UrlShortener.Application.Tests.UseCases.About.Commands.Update;

public class UpdateCommandHandlerTests
{
    private readonly Mock<IAboutRepository> _aboutRepositoryMock = new();
    private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock = new();
    private readonly UpdateCommandHandler _handler;

    public UpdateCommandHandlerTests()
    {
        _handler = new UpdateCommandHandler(
            _aboutRepositoryMock.Object,
            _userRoleRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_User_Is_Not_Admin()
    {
        var callerId = new UserId(Guid.NewGuid());
        var command = new UpdateCommand(new AboutId(Guid.NewGuid()), "Update", callerId);

        _userRoleRepositoryMock.Setup(r => r.GetByUserIdAsync(callerId))
            .ReturnsAsync(new List<UserRole>
            {
                new() { Role = new Role { Name = "User" } }
            });

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("Authorization.Failed", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_About_Not_Found()
    {
        var callerId = new UserId(Guid.NewGuid());
        var aboutId = new AboutId(Guid.NewGuid());
        var command = new UpdateCommand(aboutId, "New text", callerId);

        _userRoleRepositoryMock.Setup(r => r.GetByUserIdAsync(callerId))
            .ReturnsAsync(new List<UserRole>
            {
                new() { Role = new Role { Name = "Admin" } }
            });

        _aboutRepositoryMock.Setup(r => r.GetByIdAsync(aboutId))
            .ReturnsAsync((Domain.Models.AboutModel.About?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("About.NotFound", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Update_And_Return_Success_When_User_Is_Admin_And_About_Exists()
    {
        var callerId = new UserId(Guid.NewGuid());
        var aboutId = new AboutId(Guid.NewGuid());
        const string description = "New Description";
        var about = new Domain.Models.AboutModel.About { Id = aboutId, Description = "Old" };

        var command = new UpdateCommand(aboutId, description, callerId);

        _userRoleRepositoryMock.Setup(r => r.GetByUserIdAsync(callerId))
            .ReturnsAsync(new List<UserRole>
            {
                new() { Role = new Role { Name = "Admin" } }
            });

        _aboutRepositoryMock.Setup(r => r.GetByIdAsync(aboutId))
            .ReturnsAsync(about);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(description, about.Description);

        _aboutRepositoryMock.Verify(r => r.Update(about), Times.Once);
    }
}
