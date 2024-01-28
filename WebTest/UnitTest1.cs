using Moq;
using BazhkoTarchyla.BookLibrary.BL;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Models;

namespace WebTest
{
    public class UnitTest1
    {
        [Fact]
        public void Books_ReturnsViewResult_WithListOfBooks()
        {
            // Arrange
            var mockBLC = new Mock<BLC>();
            mockBLC.Setup(blc => blc.GetAllBooks()).Returns(GetTestBooks());
            var controller = new HomeController();

            // Act
            var result = controller.Books();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Interfaces.IBook>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }

        private IEnumerable<Interfaces.IBook> GetTestBooks()
        {
            var mockBook1 = new Mock<Interfaces.IBook>();
            var mockBook2 = new Mock<Interfaces.IBook>();

            return new List<Interfaces.IBook> { mockBook1.Object, mockBook2.Object };
        }

        [Fact]
        public void Libraries_ReturnsViewResult_WithListOfLibraries()
        {
            // Arrange
            var mockBLC = new Mock<BLC>();
            mockBLC.Setup(blc => blc.GetAllLibraries()).Returns(GetTestLibraries());

            var controller = new HomeController();

            // Act
            var result = controller.Libraries();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Interfaces.ILibrary>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());

        }

        private IEnumerable<Interfaces.ILibrary> GetTestLibraries()
        {
            var mockLibrary = new Mock<Interfaces.ILibrary>();
            var mockLibrary2 = new Mock<Interfaces.ILibrary>();

            return new List<Interfaces.ILibrary> { mockLibrary.Object, mockLibrary2.Object };
        }

        [Fact]
        public void CreateBook_ReturnsViewResult_WithBookModel()
        {
            // Arrange
            var mockBLC = new Mock<BLC>();
            mockBLC.Setup(blc => blc.GetAllLibraries()).Returns(GetTestLibraries());
            var controller = new HomeController();

            // Act
            var result = controller.CreateBook();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.NotNull(viewResult.ViewData["Genres"]);
            Assert.NotNull(viewResult.ViewData["Libraries"]);
        }
    }
}