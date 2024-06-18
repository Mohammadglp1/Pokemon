using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Services.Queries;

namespace ThePokemonProject.Services.Handlers
{
    public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, PokemonDto>
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public GetPokemonQueryHandler(IMapper mapper, IPokemonRepository pokemonRepository)
        {
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;


        }


        public  Task<PokemonDto> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {
            if (!_pokemonRepository.PokemonExists(request.Id))
                return null;
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(request.Id));

          //  return await
               return  Task.FromResult(pokemon);
            // var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

        }
    }


}
