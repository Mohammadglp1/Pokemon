using AutoMapper;
using ThePokemonProject.Dto;
using ThePokemonProject.Models;
using static CreatePokemonEndpoint;

namespace ThePokemonProject.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<CreatePokemonRequestBody, Pokemon>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

        }
    }
}
