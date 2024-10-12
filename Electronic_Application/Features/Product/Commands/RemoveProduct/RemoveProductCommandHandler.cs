using AutoMapper;
using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Product.Commands.RemoveProduct
{

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
    {
      
        private readonly IMapper _mapper;
        // private readonly IEmailService _emailService;
        private readonly IProductRepository _productRepository;
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly ILogger<RemoveProductCommand> _logger;
      

        public RemoveProductCommandHandler(IProductRepository productRepository, IMapper mapper, ILogger<RemoveProductCommand> logger, IFirebaseStorageService firebaseStorageService)
        {            
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _firebaseStorageService = firebaseStorageService ?? throw new ArgumentNullException(nameof(firebaseStorageService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var producttoRemoved = await _dbContext.Products.FirstOrDefaultAsync(w => w.Id == request.Id);
                var producttoRemoved = await _productRepository.GetByIdAsync(request.Id);
                if (producttoRemoved != null)
                {
                    await _firebaseStorageService.DeleteBlob(producttoRemoved.Image);
                    producttoRemoved.IsDeleted = true;
                    // await _dbContext.SaveChangesAsync(cancellationToken);
                    await _productRepository.DeleteAsync(producttoRemoved);
                    return true;
                }
                throw new NotFoundException(nameof(Product), request.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
