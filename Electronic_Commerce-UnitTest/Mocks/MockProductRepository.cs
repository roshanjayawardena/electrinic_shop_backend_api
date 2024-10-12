using AutoFixture;
using Electronic_Application.Contracts.Persistence;
using Moq;

namespace Electronic_Commerce_UnitTest.Mocks
{
    public static class MockProductRepository
    {

        public static Mock<IProductRepository> GenerateAllProductsRepository()
        {

            var fixture = new Fixture();

            fixture.Customize<Electronic_Domain.Entities.Product>(e => e
                                            .With(x => x.Id)
                                            .With(x => x.Name)
                                            .With(x => x.Price)
                                            .With(x => x.Brand)
                                            .With(x => x.Description)
                                            .With(x => x.CategoryId)
                                            .With(x => x.Image));         
                                                 
            var products = fixture.CreateMany<Electronic_Domain.Entities.Product>(3).ToList();

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetAllProducts()).ReturnsAsync(products);

            //mockRepo.Setup(r => r.AddAsync(It.IsAny<PropertyManagement.Domain.Entities.Property>())).ReturnsAsync((PropertyManagement.Domain.Entities.Property propertyType) =>
            //{
            //    properties.Add(propertyType);
            //    propertyType.Id = 100;
            //    return propertyType;
            //});

            return mockRepo;
        }

        public static Mock<IProductRepository> GenerateProductRepository()
        {

            var fixture = new Fixture();

            fixture.Customize<Electronic_Domain.Entities.Product>(e => e
                                            .With(x => x.Id)
                                            .With(x => x.Name)
                                            .With(x => x.Price)
                                            .With(x => x.Brand)
                                            .With(x => x.Description)
                                            .With(x => x.CategoryId)
                                            .With(x => x.Image));

            var product = fixture.Create<Electronic_Domain.Entities.Product>();

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetProductById(It.IsAny<Guid>())).ReturnsAsync(product);          

            return mockRepo;
        }

    }
}
