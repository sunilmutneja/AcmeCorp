using Application.Interfaces;
using Moq;
using WebApi.Controllers;

namespace UnitTest
{
    public class CustomerControllerTests
    {     
        [Fact]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = controller.GetAll();
        }

        [Fact]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = controller.DeleteCustomer(10);

        }

        [Fact]
        public void GetReturnsWithSameId()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);

            mockService.Setup(x => x.GetByIdAsync(42));          
            // Act
            var actionResult = controller.GetCustomerID(2);           
        }

    }
}
