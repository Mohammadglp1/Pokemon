using AutoMapper;
using MediatR;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Repositories;
using ThePokemonProject.Services.Commands;

namespace ThePokemonProject;

public class UpdatePokemonCommandHandler : IRequestHandler<UpdatePokemonCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IPokemonRepository _pokemonRepository;

    public UpdatePokemonCommandHandler(IMapper mapper, IPokemonRepository pokemonRepository)
    {
        _mapper = mapper;
        _pokemonRepository = pokemonRepository;

    }
  public async Task<bool> Handle(UpdatePokemonCommand request, CancellationToken cancellationToken)
    {
        var pokemonMap = _mapper.Map<Pokemon>(request.PokemonUpdate);
        return _pokemonRepository.UpdatePokemon(request.ownerId, request.categoryId, pokemonMap);

    }
}




