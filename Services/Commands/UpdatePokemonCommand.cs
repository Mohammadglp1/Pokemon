   using MediatR;
using ThePokemonProject.Dto;
public class UpdatePokemonCommand : IRequest<bool>
    {
       public int pokeId { get; set; }
       public int ownerId {get;set;}
       public int categoryId {get;set;}
       public PokemonDto PokemonUpdate {get;set;}
    public UpdatePokemonCommand(int pokeId, int ownerId, int categoryId, PokemonDto pokemonUpdate)
    {
        pokeId = pokeId;
        ownerId = ownerId;
        categoryId = categoryId;
        PokemonUpdate = pokemonUpdate;
    }
    }
