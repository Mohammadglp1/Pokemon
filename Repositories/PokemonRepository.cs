using ThePokemonProject.Data;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

namespace ThePokemonProject.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _dataContext;
        public PokemonRepository(DataContext context)
        {
            _dataContext = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var PokemonOwnerEntity = _dataContext.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _dataContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Pokemon = pokemon,
                Owner = PokemonOwnerEntity
            };
            _dataContext.Add(pokemonOwner);
            var pokemonCategory = new PokemonCategory()
            {
                Pokemon = pokemon,
                Category = category
            };
            _dataContext.Add(pokemonCategory);
            _dataContext.Add(pokemon);
            return Save();
        }

        public bool DeletePokemon(PokemonDto pokemon)
        {
           _dataContext.Remove(pokemon);
            return Save();
        }

     

        public Pokemon GetPokemon(int id)
        {
            return _dataContext.Pokemons.Where(p => p.Id == id).FirstOrDefault();

        }

        public Pokemon GetPokemon(string name)
        {
            return _dataContext.Pokemons.FirstOrDefault(p => p.Name == name);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _dataContext.Reviews.Where(p => p.Pokemon.Id == pokeId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _dataContext.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _dataContext.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _dataContext.Update(pokemon);
            return Save();
        }
    }
}
