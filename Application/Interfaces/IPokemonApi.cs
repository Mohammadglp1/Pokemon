using RestEase;
using ThePokemonProject.Dto;

namespace ThePokemonProject.Interfaces;

public interface IPokemonApi
{
     [Get("pokemon/{id}")]
    Task<PokemonDto> GetPokemonAsync([Path] int id);

    [Post("pokemon/{id}")]
    Task<PokemonDto> CreatePokemonAsync([Path] int id);

    [Put("pokemon/{id}")]
    Task<PokemonDto> UpdatePokemonAsync([Path] int id);

    [Delete("pokemon/{id}")]
    Task<PokemonDto> DeletePokemonAsync([Path] int id);
}

