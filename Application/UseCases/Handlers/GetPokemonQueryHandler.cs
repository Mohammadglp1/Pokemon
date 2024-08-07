﻿using AutoMapper;
using MediatR;

using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Services.Queries;
using PokemonDto = ThePokemonProject.Dto.PokemonDto;

namespace ThePokemonProject.Services.Handlers
{
    public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, IResultWrapper<PokemonDto>>
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public GetPokemonQueryHandler(IMapper mapper, IPokemonRepository pokemonRepository)
        {
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
        }
        public async Task<IResultWrapper<PokemonDto>> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
        {
            if (!_pokemonRepository.PokemonExists(request.Id))
                return ResultWrapper<PokemonDto>.Empty();
            
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(request.Id));

            return  ResultWrapper<PokemonDto>.Create(pokemon, 600);
        }

    }


}
