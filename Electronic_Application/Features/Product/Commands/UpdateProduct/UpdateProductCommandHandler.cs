using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
       {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly ILogger<UpdateProductCommand> _logger;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductCommand> logger, IFirebaseStorageService firebaseStorageService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _firebaseStorageService = firebaseStorageService ?? throw new ArgumentNullException(nameof(firebaseStorageService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           
            var productEntity = await _productRepository.GetByIdAsync(request.Id);           

            if (productEntity != null) {

                var isExist = await _firebaseStorageService.CheckExistOrNot(productEntity.Image);
                if (isExist && request.Image != null)
                {
                    await _firebaseStorageService.DeleteBlob(productEntity.Image);
                    var photoUri = await _firebaseStorageService.UploadFile(request.Image);
                    productEntity.Image = photoUri.ToString();
                }
                productEntity.Name= request.Name;
                productEntity.Brand= request.Brand;
                productEntity.Description= request.Description;
                productEntity.Price= request.Price;               
                productEntity.CategoryId= request.CategoryId;

                await _productRepository.UpdateAsync(productEntity);
                return request.Id;
            }
            throw new NotFoundException(nameof(Product), request.Id);         
        }
    }
}
