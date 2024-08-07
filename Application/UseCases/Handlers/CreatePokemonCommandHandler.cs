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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPokemonRepository _pokemonRepository;
    

    public CreatePokemonCommandHandler(IMapper mapper,ICategoryRepository categoryRepository,IPokemonRepository pokemonRepository )
{
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _pokemonRepository = pokemonRepository;
        
    }
    public async Task<int> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
                  if (!_categoryRepository.CategoryExists(request.CreatePokemonRequestBody.CategoryId))
        {
            throw new CategoryExistsException("Category doesnt exist.");
        }
        var pokemonMap = _mapper.Map<Pokemon>(request.CreatePokemonRequestBody);
        _pokemonRepository.CreatePokemon(request.CreatePokemonRequestBody.Name,request.CreatePokemonRequestBody.BirthDate,request.CreatePokemonRequestBody.OwnerId,request.CreatePokemonRequestBody.CategoryId,pokemonMap);
        return pokemonMap.Id;
    }
}
