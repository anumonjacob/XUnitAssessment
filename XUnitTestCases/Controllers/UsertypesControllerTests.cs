using AutoFixture;
using Moq;
using XUnitAssessment.Services.Interface;
using XUnitAssessment.Controllers;
using XUnitAssessment.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Common;

namespace XUnitTestCases.Controllers
{
    public class UsertypesControllerTests
    {
        private readonly IFixture fixture;
        public readonly Mock<IUserType> UserInterface;
        public readonly UsertypesController UserController;


        public UsertypesControllerTests()
        {
            fixture = new Fixture();
            UserInterface = fixture.Freeze<Mock<IUserType>>();
            UserController = new UsertypesController(UserInterface.Object);
        }

        //TestCases for Add Usertype
        [Fact]
        public void AddUserType_ShouldReturnOk_WhenUsertypeIsNotNull()
        {
            //Arrange
            var usertype = fixture.Create<Usertype>();
            var ReturnData = fixture.Create<Usertype>();
            UserInterface.Setup(s => s.AddUserType(usertype)).ReturnsAsync(ReturnData);

            //Act
            var Response = UserController.AddUserType(usertype);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            UserInterface.Verify(s => s.AddUserType(usertype), Times.Once());

        }

        [Fact]
        public void AddUserType_ShouldReturnStatusCode500_WhenUsertypeIsNull()
        {
            //Arrange
            Usertype usertype = null;
            var ReturnData = (Usertype)null;
            UserInterface.Setup(s => s.AddUserType(usertype)).ReturnsAsync(ReturnData);

            //Act
            var Response = UserController.AddUserType(usertype);

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Failed to Add Usertypes");
            UserInterface.Verify(s => s.AddUserType(It.IsAny<Usertype>()), Times.Once());

        }

        [Fact]
        public void AddUserType_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            var usertype =fixture.Create<Usertype>();
            UserInterface.Setup(s => s.AddUserType(usertype)).Throws(new Exception());

            //Act
            var Response = UserController.AddUserType(usertype);

            //Assert
            Response.Should().NotBeNull();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error Occured while Adding Usertypes");
            UserInterface.Verify(s => s.AddUserType(usertype), Times.Once());

        }

        //TestCases for GetAllUserType
        [Fact]
        public void GetAllUserType_ShouldReturnOk_WhenUsertypeIsNotNull()
        {
            //Arrange
            var Usertype = fixture.CreateMany<Usertype>();
            UserInterface.Setup(s => s.GetAllUserType()).ReturnsAsync(Usertype);

            //Act
            var Response = UserController.GetAllUserType();

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            Response.Result.Should().BeAssignableTo<OkObjectResult>();
            UserInterface.Verify(s => s.GetAllUserType(), Times.Once());
        }

        [Fact]
        public void GetAllUserType_ShouldReturnNotFound_WhenUsertypesAreNull()
        {
            //Arrange
            IEnumerable<Usertype> Usertypes = null;
            UserInterface.Setup(s => s.GetAllUserType()).ReturnsAsync(Usertypes);

            //Act
            var Response = UserController.GetAllUserType();

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("Usertype Not Found");
            UserInterface.Verify(s => s.GetAllUserType(), Times.Once());

        }

        [Fact]
        public void GetAllUserType_ShouldReturnStatusCode500_WhenExceptionOccurs()
        {
            //Arrange
            UserInterface.Setup(s => s.GetAllUserType()).Throws(new Exception());

            //Act
            var Response = UserController.GetAllUserType();

            //Assert
            Response.Should().NotBeNull();
            Response.Should().BeAssignableTo<Task<IActionResult>>();
            var objectResult = Response.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("Error Occured while getting Usertypes");
            UserInterface.Verify(s => s.GetAllUserType(), Times.Once());

        }
    }
}
