using MediatR;

namespace Electronic_Application.Features.Category.Queries.GetAllCategories
{
    public class GetCategoryListRequest : IRequest<List<CategoryDto>>
    {
    }
}
