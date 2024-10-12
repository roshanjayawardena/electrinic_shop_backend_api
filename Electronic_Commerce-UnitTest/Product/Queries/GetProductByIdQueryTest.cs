using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Application.Features.Product.Queries.GetProductById;
using Electronic_Application.Mappings;
using Electronic_Commerce_UnitTest.Mocks;
using Electronic_Domain.Entities;
using Moq;
using Shouldly;

namespace Electronic_Commerce_UnitTest.Product.Queries
{
    public class GetProductByIdQueryTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryTest()
        {
            _productRepository = new();
            _productRepository = MockProductRepository.GenerateProductRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetProductByIdQueryHandler(_productRepository.Object, _mapper);
        }


        [Theory]
        [InlineData("7523e8b2-10b6-4d97-a517-b9815f44dbe9")]
        public async Task Handle_Should_Return_Product_By_Id(string productId)
        {
            //Act
            var result = await _handler.Handle(new GetProductByIdQuery() { Id = Guid.Parse(productId)  }, CancellationToken.None);

            //Assert
            _productRepository.Verify(x => x.GetProductById(It.IsAny<Guid>()), Times.Once);
            result.ShouldBeOfType<ProductDto>();         
        }

        [Fact]      
        public async Task Handle_Should_Return_Validation_exception_When_Invalid_Id()
        {
            ////Arrange        
            //_productRepository.Setup(r => r.GetProductById(It.IsAny<Guid>()))
            //                  .ThrowsAsync(new ValidationException("ProductId", "Invalid product id."));
           
            //Act && Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new GetProductByIdQuery() { Id = Guid.Empty }, CancellationToken.None));
           
        }

        [Fact]
        public async Task Handle_Should_Return_Not_Found_exception_When_Product_Is_Null()
        {
            Electronic_Domain.Entities.Product productDomain = null;
            //Arrange        
            _productRepository.Setup(r => r.GetProductById(It.IsAny<Guid>()))
                              .ReturnsAsync(productDomain);

            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(new GetProductByIdQuery() { Id = Guid.NewGuid() }, CancellationToken.None));

        }
    }
}
