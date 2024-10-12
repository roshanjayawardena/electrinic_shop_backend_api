using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Features.Product.Commands.CreateProduct;
using Electronic_Application.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Text;

namespace Electronic_Commerce_UnitTest.Product.Commands
{
    public class CreateProductCommandTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IFirebaseStorageService> _storageService;
        private readonly Mock<ILogger<CreateProductCommand>> _logger;
        private readonly IMapper _mapper;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandTest()
        {
            _logger = new();          
            _storageService = new();
            _productRepository = new();
             var mapperConfig = new MapperConfiguration(c =>
             {
                c.AddProfile<MappingProfile>();
             });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateProductCommandHandler(_productRepository.Object, _mapper, _logger.Object, _storageService.Object);
        }

        [Theory]
        [InlineData("e5224d80-0903-4754-98cb-c77d6b2feaa5")]
        public async Task Handle_Should_Return_Success_result(Guid productId) 
        {
            //Arrange
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");

            _productRepository.Setup(r => r.AddAsync(It.IsAny<Electronic_Domain.Entities.Product>()))
                       .ReturnsAsync((Electronic_Domain.Entities.Product product) =>
                       {
                          product.Id = Guid.Parse("e5224d80-0903-4754-98cb-c77d6b2feaa5");
                          return product;
                       });

            _storageService.Setup(r=>r.UploadFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(new Uri("https://storage.googleapis.com/download/storage/v1/b/electronic-shop-9fe85.appspot.com/o/eshop%2Fdbd0eaa7-47b2-4957-96f0-69b922a1c63c.jpg?generation=1688890598720105&alt=media"));

            var command = new CreateProductCommand()
            {
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
            _productRepository.Verify(x => x.AddAsync(It.Is<Electronic_Domain.Entities.Product>(m => m.Id == result)),Times.Once);           
            result.ShouldBeOfType<Guid>();
            Assert.Equal(result, productId);
        }

        [Fact]        
        public async Task Handle_Should_Return_Failure_result_When_Database_Error()
        {
            //Arrange        
            _productRepository.Setup(r => r.AddAsync(It.IsAny<Electronic_Domain.Entities.Product>()))
                              .ThrowsAsync(new Exception("Simulated database exception"));                     

            _storageService.Setup(r => r.UploadFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(new Uri("https://storage.googleapis.com/download/storage/v1/b/electronic-shop-9fe85.appspot.com/o/eshop%2Fdbd0eaa7-47b2-4957-96f0-69b922a1c63c.jpg?generation=1688890598720105&alt=media"));

            var command = new CreateProductCommand()
            {
                Name = "Araliya Resort",
                Description = "No 18/A Temple road,Colombo",
                Price = 2000,
                Brand = "",
                CategoryId = Guid.Parse("606c9622-239e-49c2-acba-c69a4f24b6c9"),
                // Image  = 
            };      

            //Act && Assert
            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));           
        }
    }
}
