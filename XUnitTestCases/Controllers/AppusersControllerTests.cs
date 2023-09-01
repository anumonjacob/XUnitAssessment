using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitAssessment.Controllers;
using XUnitAssessment.Models;
using XUnitAssessment.Services.Interface;

namespace XUnitTestCases.Controllers
{
    public class AppusersControllerTests
    {
        private readonly IFixture fixture;
        public readonly Mock<IAppuser> AppInterface;
        public readonly AppusersController AppController;

        public AppusersControllerTests()
        {
            fixture = new Fixture();
            AppInterface = fixture.Freeze<Mock<IAppuser>>();
            AppController = new AppusersController(AppInterface.Object);
        }

        //TestCases for Add Appuser
        [Fact]
        public void AddAppuser_ShouldReturnOk_WithValidPayload()
        {
            //Arrange
            var AppUser = fixture.Create<Appuser>();
            var ReturnData = fixture.Create<Appuser>();
            AppInterface.Setup(s => s.AddAppuser(AppUser)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.AddAppuser(AppUser);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            AppInterface.Verify(s => s.AddAppuser(AppUser), Times.Once());

        }

        [Fact]
        public void AddAppuser_ShouldReturnStatusCode500_WhenAppUserIsNull()
        {
            Appuser AppUser = null;

            //Act
            var Response = AppController.AddAppuser(AppUser);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Failed to Add AppUser");
            AppInterface.Verify(s => s.AddAppuser(It.IsAny<Appuser>()), Times.Once());

        }

        [Fact]
        public void AddAppuser_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            var AppUser = fixture.Create<Appuser>();
            AppInterface.Setup(s => s.AddAppuser(AppUser)).Throws(new Exception());

            //Act
            var Response = AppController.AddAppuser(AppUser);

            //Assert
            Response.Should().NotBeNull();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error occured While Adding AppUser");
            AppInterface.Verify(s => s.AddAppuser(AppUser), Times.Once());

        }

        //TestCases for GetAllAppuserByUserType
        [Fact]
        public void GetAllAppuserByUserType_ShouldReturnOk_WhenUsertypeIsNotNull()
        {
            //Arrange
            var Usertypes = "Admin";
            var ReturnData = fixture.CreateMany<Appuser>();
            AppInterface.Setup(s => s.GetAllAppuserByUserType(Usertypes)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.GetAllAppuserByUserType(Usertypes);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            AppInterface.Verify(s => s.GetAllAppuserByUserType(Usertypes), Times.Once());
        }

        [Fact]
        public void GetAllAppuserByUserType_ShouldReturnStatusCode404_WhenUsertypesAreNull()
        {
            //Arrange
            string Usertypes = null;
            var ReturnData = fixture.CreateMany<Appuser>();
            AppInterface.Setup(s => s.GetAllAppuserByUserType(Usertypes)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.GetAllAppuserByUserType(Usertypes);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("userType cannot be null");
            AppInterface.Verify(s => s.GetAllAppuserByUserType(Usertypes), Times.Never());

        }

        [Fact]
        public void GetAllAppuserByUserType_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            var Usertypes = "Admin";
            AppInterface.Setup(s => s.GetAllAppuserByUserType(Usertypes)).Throws(new Exception());

            //Act
            var Response = AppController.GetAllAppuserByUserType(Usertypes);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error Occured while Getting Appuser by Usertype");
            AppInterface.Verify(s => s.GetAllAppuserByUserType(Usertypes), Times.Once());

        }

        [Fact]
        public void UpdateAppuser_ShouldReturnOk_WithValidPayload()
        {
            //Arrange
            var AppUserId = 1;
            var appUser = new Appuser
            {
                UserName = "Test"
            };
            var ReturnData = new Appuser
            {
                AppUserId = 1,
                UserName = "admin1"
            };
            AppInterface.Setup(s => s.UpdateAppuser(AppUserId, appUser)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.UpdateAppuser(AppUserId, appUser);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            AppInterface.Verify(s => s.UpdateAppuser(AppUserId, appUser), Times.Once());
        }

        [Fact]
        public void UpdateAppuser_ShouldReturnStatusCode500_WhenAppuserIsNull()
        {
            //Arrange
            var AppUserId = 1;
            var appUser = (Appuser)null;
            var ReturnData = new Appuser
            {
                AppUserId = 1,
                UserName = "admin1"
            };
            AppInterface.Setup(s => s.UpdateAppuser(AppUserId, appUser)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.UpdateAppuser(AppUserId, appUser);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("AppUser cannot be null");
            AppInterface.Verify(s => s.UpdateAppuser(AppUserId, appUser), Times.Never());
        }

        [Fact]
        public void UpdateAppuser_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            var AppUserId = 1;
            var appUser = new Appuser
            {
                UserName = "Test"
            };
            var ReturnData = new Appuser
            {
                AppUserId = 1,
                UserName = "admin1"
            };
            AppInterface.Setup(s => s.UpdateAppuser(AppUserId, appUser)).Throws(new Exception());

            //Act
            var Response = AppController.UpdateAppuser(AppUserId, appUser);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error Occured while Updating AppUser");
            AppInterface.Verify(s => s.UpdateAppuser(AppUserId, appUser), Times.Once());
        }

        [Fact]
        public void DeleteAppuser_ShouldReturnOk_WithAppUserIdNotNull()
        {
            //Arrange
            var AppUserId = 1;
            var ReturnData = fixture.Create<Appuser>();
            AppInterface.Setup(s => s.DeleteAppuser(AppUserId)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.DeleteAppuser(AppUserId);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            AppInterface.Verify(s => s.DeleteAppuser(AppUserId), Times.Once());
        }

        [Fact]
        public void DeleteAppuser_ShouldReturnNotFound_WithAppUserIdNotNull()
        {
            //Arrange
            var AppUserId = 12;
            var ReturnData = (Appuser)null;
            AppInterface.Setup(s => s.DeleteAppuser(AppUserId)).ReturnsAsync(ReturnData);

            //Act
            var Response = AppController.DeleteAppuser(AppUserId);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("Appuser Not Found");
            AppInterface.Verify(s => s.DeleteAppuser(AppUserId), Times.Once());
        }

        [Fact]
        public void DeleteAppuser_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            var AppUserId = 1;
            var ReturnData = fixture.Create<Appuser>();
            AppInterface.Setup(s => s.DeleteAppuser(AppUserId)).Throws(new Exception());

            //Act
            var Response = AppController.DeleteAppuser(AppUserId);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error Occured while Deleting AppUser");
            AppInterface.Verify(s => s.DeleteAppuser(AppUserId), Times.Once());
        }

    }
}
