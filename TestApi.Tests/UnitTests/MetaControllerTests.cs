using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.Controllers;
using TestApi.Servicios;

namespace TestApi.Tests.UnitTests
{
    [TestClass]
    public class MetaControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsLogMessage() 
        {
            //Arrange
            string logMessage = "Log message";
            Mock<ILoggService> loggServiceMock = new Mock<ILoggService>();
            loggServiceMock.Setup(l=>l.ReadLog()).ReturnsAsync(logMessage);

            var metaController = new MetaController(loggServiceMock.Object);
            //Act
            var result = await metaController.Get();
            //Assert
            OkObjectResult okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(logMessage, okObjectResult.Value);
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
            loggServiceMock.Verify(s => s.ReadLog(), Times.Once);
        }
        [TestMethod]
        public async Task Get_ReturnsInternalServerError() 
        {
            //Arrange
            string errorMessage = "Internal Server Error";
            Mock<ILoggService> loggServiceMock = new Mock<ILoggService>();
            loggServiceMock.Setup(s => s.ReadLog()).ThrowsAsync(new Exception(errorMessage));

            var metaController = new MetaController(loggServiceMock.Object);
            //Act

            var result = await metaController.Get();

            //Assert

            ObjectResult objectResult = (ObjectResult)result.Result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual(errorMessage, objectResult.Value);
            loggServiceMock.Verify(s => s.ReadLog(), Times.Once);
        }
        [TestMethod]
        public async Task Truncate_ReturnsSuccessTruncateFile() 
        {
            //Arrange
            string message = "log truncado";
            Mock<ILoggService> loggServiceMock = new Mock<ILoggService>();
            loggServiceMock.Setup(s => s.TruncateLog()).ReturnsAsync(message);
            var metaController = new MetaController(loggServiceMock.Object);
            //Act
            var result = await metaController.Truncate();
            //Assert
            ObjectResult ObjectResult = (ObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, ObjectResult.StatusCode);
            dynamic data = ObjectResult.Value;
            var codeValue = data.GetType().GetProperty("code").GetValue(data, null);
            var messageValue = data.GetType().GetProperty("message").GetValue(data, null);
            Assert.AreEqual(StatusCodes.Status200OK, codeValue);
            Assert.AreEqual(message, messageValue);
        }
        [TestMethod]
        public async Task Truncate_ReturnsInternalServerError()
        {
            // Arrange
            string errorMessage = "Internal Server Error";
            Mock<ILoggService> loggServiceMock = new Mock<ILoggService>();
            loggServiceMock.Setup(s => s.TruncateLog()).ThrowsAsync(new Exception(errorMessage));

            MetaController metaController = new MetaController(loggServiceMock.Object);

            // Act
            ActionResult result = await metaController.Truncate();

            // Assert
            ObjectResult objectResult = (ObjectResult)result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            loggServiceMock.Verify(s => s.TruncateLog(), Times.Once);
        }
    }
}
