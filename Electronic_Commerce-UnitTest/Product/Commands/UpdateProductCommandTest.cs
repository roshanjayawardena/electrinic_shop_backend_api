using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using Electronic_Application.Features.Product.Commands.UpdateProduct;
using Electronic_Application.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Text;

namespace Electronic_Commerce_UnitTest.Product.Commands
{
    public class UpdateProductCommandTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IFirebaseStorageService> _storageService;
        private readonly Mock<ILogger<UpdateProductCommand>> _logger;
        private readonly IMapper _mapper;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandTest()
        {
            _logger = new();
            _storageService = new();
            _productRepository = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateProductCommandHandler(_productRepository.Object, _mapper, _logger.Object, _storageService.Object);
        }


        [Theory]
        [InlineData("e5224d80-0903-4754-98cb-c77d6b2feaa5")]
        public async Task Handle_Should_Return_Success_result(Guid productId)
        {
            //Arrange
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");

            _productRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                       .ReturnsAsync(new Electronic_Domain.Entities.Product() { 
                          Id= productId,
                          Image = "https://storage.googleapis.com/download/storage/v1/b/electronic-shop-9fe85.appspot.com/o/eshop%2Fdbd0eaa7-47b2-4957-96f0-69b922a1c63c.jpg?generation=1688890598720105&alt=media",
                          Name = "test-item-01",
                          Price = 4000
                       });

            _productRepository.Setup(r => r.UpdateAsync(It.IsAny<Electronic_Domain.Entities.Product>()))
                       .Returns(Task.CompletedTask);

            _storageService.Setup(r => r.CheckExistOrNot(It.IsAny<string>()))
               .ReturnsAsync(true);

            _storageService.Setup(r => r.DeleteBlob(It.IsAny<string>()))
               .Returns(Task.CompletedTask);

            _storageService.Setup(r => r.UploadFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(new Uri("https://storage.googleapis.com/download/storage/v1/b/electronic-shop-9fe85.appspot.com/o/eshop%2Fdbd0eaa7-47b2-4957-96f0-69b922a1c63c.jpg?generation=1688890598720105&alt=media"));

            var command = new UpdateProductCommand()
            {
                Id = productId,
                Name = "Araliya Resort",
                Description = "No 18/A Temple road,Colombo",
                Price = 2000,
                Brand = "",
                CategoryId = Guid.Parse("606c9622-239e-49c2-acba-c69a4f24b6c9"),
                Image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt")
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            _productRepository.Verify(x => x.UpdateAsync(It.Is<Electronic_Domain.Entities.Product>(m => m.Id == result)), Times.Once);
            result.ShouldBeOfType<Guid>();
            Assert.Equal(result, productId);
        }

        [Fact]
        public async Task Handle_Should_Return_Not_Found_exception_When_Product_Is_Null()
        {
          
            //Arrange
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");

            Electronic_Domain.Entities.Product productDomain = null;

            _productRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(productDomain);

            var command = new UpdateProductCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Araliya Resort",
                Description = "No 18/A Temple road,Colombo",
                Price = 2000,
                Brand = "",
                CategoryId = Guid.Parse("606c9622-239e-49c2-acba-c69a4f24b6c9"),
                Image = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt")
            };

            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));

        }
    }
}
