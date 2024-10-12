using AutoMapper;
using Electronic_Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Electronic_Application.Features.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
       // private readonly IEmailService _emailService;
        private readonly ILogger<CreateCategoryCommand> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CreateCategoryCommand> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var propertyEntity = _mapper.Map<Electronic_Domain.Entities.Category>(request);
            var newProperty = await _categoryRepository.AddAsync(propertyEntity);
            _logger.LogInformation($"Property {newProperty.Id} is successfully created.");   
            return newProperty.Id;
        }
    }
}
