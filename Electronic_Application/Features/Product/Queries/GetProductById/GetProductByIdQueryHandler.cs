using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Exceptions;
using FluentValidation.Results;
using MediatR;

namespace Electronic_Application.Features.Product.Queries.GetProductById
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;        
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ValidationException(new ValidationFailure("ProductId", "Invalid product id."));
            }
            var product = await _productRepository.GetProductById(request.Id);

            if (product == null) { throw new NotFoundException("Product is not found"); }

            return _mapper.Map<ProductDto>(product);
        }
    }
}
