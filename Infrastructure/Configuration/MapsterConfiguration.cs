using Mapster;
using ThePokemonProject.Dto;
using ThePokemonProject.Models;

public static class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Pokemon, PokemonDto>
            .NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.BirthDate, src => src.BirthDate);
          

        TypeAdapterConfig<Category, CategoryDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
             .Map(dest => dest.Name, src => src.Name);}}
             

     