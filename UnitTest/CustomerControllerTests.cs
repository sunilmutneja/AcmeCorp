using Application.Interfaces;
using Domain.Entities;
using Moq;
using WebApi.Controllers;

namespace UnitTest
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task CustomerController_Get_NoCustomerAsync()
        {
            //Arrange
            var mockRepo = new Mock<IApplicationDbContext>();

            var controller = new CustomerController((mockRepo.Object));

            //Act
   
            var result = await controller.Get();

            //Assert

            var viewResult = Assert.IsAssignableFrom<IAsyncEnumerable<Customer>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(
                viewResult.GetAsyncEnumerator());
            Assert.Equal(2, model.Count());
        }

    }
}
