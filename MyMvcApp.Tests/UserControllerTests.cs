using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;

namespace MyMvcApp.Tests;

public class UserControllerTests
{
    [Fact]
    public void Index_ReturnsViewWithUserList()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<User>>(result.Model);
        var model = result.Model as List<User>;
        Assert.Single(model);
    }

    [Fact]
    public void Details_ReturnsViewWithUser_WhenUserExists()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        // Act
        var result = controller.Details(1) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<User>(result.Model);
        var model = result.Model as User;
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public void Details_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var controller = new UserController();

        // Act
        var result = controller.Details(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_AddsUserAndRedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new UserController();
        var newUser = new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" };

        // Act
        var result = controller.Create(newUser) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Contains(newUser, UserController.userlist);
    }

    [Fact]
    public void Edit_UpdatesUserAndRedirectsToIndex_WhenModelStateIsValid()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });
        var updatedUser = new User { Id = 1, Name = "John Updated", Email = "john.updated@example.com" };

        // Act
        var result = controller.Edit(1, updatedUser) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        var user = UserController.userlist.FirstOrDefault(u => u.Id == 1);
        Assert.Equal("John Updated", user.Name);
    }

    [Fact]
    public void Delete_RemovesUserAndRedirectsToIndex_WhenUserExists()
    {
        // Arrange
        var controller = new UserController();
        UserController.userlist.Add(new User { Id = 1, Name = "John Doe", Email = "john@example.com" });

        // Act
        var result = controller.Delete(1, null) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Index", result.ActionName);
        Assert.Empty(UserController.userlist);
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var controller = new UserController();

        // Act
        var result = controller.Delete(999, null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}