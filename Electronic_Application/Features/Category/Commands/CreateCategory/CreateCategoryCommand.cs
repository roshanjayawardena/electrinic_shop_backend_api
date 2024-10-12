using MediatR;

namespace Electronic_Application.Features.Category.Commands.CreateCategory
{
    public class CreateCategoryCommand: IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
