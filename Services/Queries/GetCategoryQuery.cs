using MediatR;
using ThePokemonProject.Models;

namespace ThePokemonProject.Services.Queries
{
    public class GetCategoryQuery : IRequest<Category>

    {
        public int Id { get; set; }
        public GetCategoryQuery(int catId)
        {
            Id = catId;
        }
    }
}
