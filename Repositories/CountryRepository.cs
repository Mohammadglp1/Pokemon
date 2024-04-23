using AutoMapper;
using ThePokemonProject.Data;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

namespace ThePokemonProject.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CountryRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public bool CountryExists(int id)
        {
            return _dataContext.Countries.Any(p => p.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _dataContext.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _dataContext.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _dataContext.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _dataContext.Countries.FirstOrDefault(c => c.Id == id);
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _dataContext.Owners.FirstOrDefault(o => o.Id == ownerId).Country;
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _dataContext.Owners.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
           _dataContext.Update(country);
           return  Save();
        }
    }
}
