using MediatR;
using ThePokemonProject.Models;

namespace ThePokemonProject.Services.Queries
{
    public class GetOwnerQuery:IRequest<Owner>
    {public int Id { get; }
        public GetOwnerQuery(int ownerId)
        {
            Id = ownerId;
        }
    }
}
