using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Repositories;

namespace ThePokemonProject;



public class CreatePokemonCommandHandler:IRequestHandler<CreatePokemonCommand,int>
{
    private readonly IMapper _mapper;
    private readonly IPokemonRepository _pokemonRepository;
    

    public CreatePokemonCommandHandler(IMapper mapper,IPokemonRepository pokemonRepository )
{
        _mapper = mapper;
        _pokemonRepository = pokemonRepository;
        
    }
    public async Task<int> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
              // Save the Pokemon
      
        var pokemonMap = _mapper.Map<Pokemon>(request.CreatePokemonRequestBody);
        _pokemonRepository.CreatePokemon(request.CreatePokemonRequestBody.OwnerId,request.CreatePokemonRequestBody.CategoryId,pokemonMap);
        return pokemonMap.Id;
    }
}
