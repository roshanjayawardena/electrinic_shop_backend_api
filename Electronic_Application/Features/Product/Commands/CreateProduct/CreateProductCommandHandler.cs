using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IFirebaseStorageService _firebaseStorageService;
        // private readonly IEmailService _emailService;
        private readonly ILogger<CreateProductCommand> _logger;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<CreateProductCommand> logger,IFirebaseStorageService firebaseStorageService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _firebaseStorageService = firebaseStorageService ?? throw new ArgumentNullException(nameof(firebaseStorageService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<Electronic_Domain.Entities.Product>(request);
            var photoUri = await _firebaseStorageService.UploadFile(request.Image);                          
            productEntity.Image = photoUri.ToString();
            var newProduct = await _productRepository.AddAsync(productEntity);
            _logger.LogInformation($"Product {newProduct.Id} is successfully created.");
            return newProduct.Id;
        }
    }
}
