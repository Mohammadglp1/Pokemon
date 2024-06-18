using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Models;

namespace ThePokemonProject.Services.Queries
{
    public class GetPokemonQuery:IRequest<PokemonDto>
       
    {public int Id { get; }
        public GetPokemonQuery(int pokeId)
        {
            Id = pokeId;
        }
    }
}
