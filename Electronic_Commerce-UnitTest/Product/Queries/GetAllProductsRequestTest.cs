using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Application.Mappings;
using Electronic_Commerce_UnitTest.Mocks;
using Moq;
using Shouldly;

namespace Electronic_Commerce_UnitTest.Product.Queries
{
    public class GetAllProductsRequestTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly GetAllProductsRequestHandler _handler;
        public GetAllProductsRequestTest()
        {
            _productRepository = new();
            _productRepository = MockProductRepository.GenerateAllProductsRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetAllProductsRequestHandler(_productRepository.Object, _mapper);
        }

        [Fact]        
        public async Task Handle_Should_Return_All_result()
        {
            //Act
            var result = await _handler.Handle(new GetAllProductsRequest(), CancellationToken.None);

            //Assert
            _productRepository.Verify(x => x.GetAllProducts(), Times.Once);
            result.ShouldBeOfType<List<ProductListDto>>();
            result.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_Should_Return_Failure_result_When_Database_Error()
        {
            //Arrange        
            _productRepository.Setup(r => r.GetAllProducts())
                              .ThrowsAsync(new Exception("Simulated database exception"));           

            //Act && Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(new GetAllProductsRequest(), CancellationToken.None));
        }
    }
}
