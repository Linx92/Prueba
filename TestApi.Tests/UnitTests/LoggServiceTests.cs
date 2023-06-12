using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;
using TestApi.Servicios;

namespace TestApi.Tests.UnitTests
{
    [TestClass]
    public class LoggServiceTests
    {
        [TestMethod]
        public async Task ReadLog_ShouldReturnsContentLogs()
        {
            // Arrange
            string contenidoEsperado = "Contenido del archivo";

            var IloggServiceMock = new Mock<ILoggService>();
            IloggServiceMock.Setup(s => s.ReadLog()).ReturnsAsync(contenidoEsperado);

            // Act
            var loggService = IloggServiceMock.Object;
            var result = await loggService.ReadLog();
            // Assert
            Assert.AreEqual(contenidoEsperado, result);
        }
        [TestMethod]
        public async Task ReadLog_ShouldReturnContentLogs_FromFile()
        {
            // Arrange
            var ruta = @"TestFiles\\content-log.txt";
            var IconfigurationMock = new Mock<IConfiguration>();
            IconfigurationMock.Setup(s => s["LOG_FILE_PATH"]).Returns(ruta);

            
            var logService = new LoggService(IconfigurationMock.Object);

            // Act
            var result = await logService.ReadLog();

            // Assert
            Assert.AreEqual("Contenido del archivo", result);
        }
        [TestMethod]
        public async Task ReadLog_ShouldReturnNoLogs_FromFile()
        {
            // Arrange
            var ruta = @"TestFiles\\nocontent-log.txt";
            var IconfigurationMock = new Mock<IConfiguration>();
            IconfigurationMock.Setup(s => s["LOG_FILE_PATH"]).Returns(ruta);


            var logService = new LoggService(IconfigurationMock.Object);

            // Act
            var result = await logService.ReadLog();

            // Assert
            Assert.AreEqual("No logs", result);
        }
        [TestMethod]
        public async Task ReadLog_ShouldReturnsNoLogs()
        {
            // Arrange
            string contenidoEsperado = "No logs";

            var IloggServiceMock = new Mock<ILoggService>();
            IloggServiceMock.Setup(s => s.ReadLog()).ReturnsAsync(contenidoEsperado);

            // Act
            // Act
            var loggService = IloggServiceMock.Object;
            var result = await loggService.ReadLog();

            // Assert
            Assert.AreEqual("No logs", result);
        }
        [TestMethod]
        public async Task TruncateLog_ShouldTruncateContentILog()
        {
            string contenidoEsperado = "log truncado";
            // Arrange

            var IloggServiceMock = new Mock<ILoggService>();
            IloggServiceMock.Setup(s => s.TruncateLog()).ReturnsAsync(contenidoEsperado);
            // Act
            var loggService = IloggServiceMock.Object;
            var result = await loggService.TruncateLog();

            // Assert
            Assert.AreEqual(contenidoEsperado, result);
        }
        [TestMethod]
        public async Task ReadLog_ShouldTrunctateContenLog()
        {
            // Arrange
            string contenidoEsperado = "log truncado";

            var ruta = @"TestFiles\\nocontent-log.txt";
            var IconfigurationMock = new Mock<IConfiguration>();
            IconfigurationMock.Setup(s => s["LOG_FILE_PATH"]).Returns(ruta);


            var logService = new LoggService(IconfigurationMock.Object);

            // Act
            var result = await logService.TruncateLog();

            // Assert
            Assert.AreEqual(contenidoEsperado, result);
        }
    }
}
