using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Product.Commands.RemoveProduct;
using Electronic_Application.Mappings;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace Electronic_Commerce_UnitTest.Product.Commands
{
    public class RemoveProductCommandTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IFirebaseStorageService> _storageService;
        private readonly Mock<ILogger<RemoveProductCommand>> _logger;
        private readonly IMapper _mapper;
        private readonly RemoveProductCommandHandler _handler;
        public RemoveProductCommandTest()
        {
            _logger = new();
            _storageService = new();
            _productRepository = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new RemoveProductCommandHandler(_productRepository.Object, _mapper, _logger.Object, _storageService.Object);
        }


        [Theory]
        [InlineData("e5224d80-0903-4754-98cb-c77d6b2feaa5")]
        public async Task Handle_Should_Return_Success_result(Guid productId)
        {
            //Arrange        
            _productRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(new Electronic_Domain.Entities.Product()
                       {
                           Id = productId,
                           Image = "https://storage.googleapis.com/download/storage/v1/b/electronic-shop-9fe85.appspot.com/o/eshop%2Fdbd0eaa7-47b2-4957-96f0-69b922a1c63c.jpg?generation=1688890598720105&alt=media",
                           Name = "test-item-01",
                           Price = 4000
                       });

            _productRepository.Setup(r => r.DeleteAsync(It.IsAny<Electronic_Domain.Entities.Product>()))
                       .Returns(Task.CompletedTask);           

            _storageService.Setup(r => r.DeleteBlob(It.IsAny<string>()))
               .Returns(Task.CompletedTask);           

            var command = new RemoveProductCommand()
            {
                Id = productId,               
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
           // _productRepository.Verify(x => x.DeleteAsync(It.Is<Electronic_Domain.Entities.Product>(x=>x.)), Times.Once);
            result.ShouldBeOfType<bool>();
            Assert.Equal(true, result);
        }

        [Theory]
        [InlineData("e5224d80-0903-4754-98cb-c77d6b2feaa5")]
        public async Task Handle_Should_Return_Not_Found_Exception_When_Product_IsNull(Guid productId)
        {
            //Arrange

            Electronic_Domain.Entities.Product productDomain = null;

            _productRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(productDomain);            

            var command = new RemoveProductCommand()
            {
                Id = productId,
            };

            //Act & Assert   
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
