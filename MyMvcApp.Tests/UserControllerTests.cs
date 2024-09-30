using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Create_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = controller.Create(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Contains(user, UserController.userlist);
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToActionResult()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };
            var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            var user = UserController.userlist.FirstOrDefault(u => u.Id == 1);
            Assert.Equal("John Smith", user.Name);
        }

        [Fact]
        public void DeleteConfirmed_ReturnsRedirectToActionResult()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.DoesNotContain(UserController.userlist, u => u.Id == 1);
        }
    }
}