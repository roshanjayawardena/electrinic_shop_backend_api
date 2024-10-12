using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using MediatR;

namespace Electronic_Application.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsRequestHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<ProductListDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetAllProducts();
            return _mapper.Map<List<ProductListDto>>(productList);           
        }
    }
}
