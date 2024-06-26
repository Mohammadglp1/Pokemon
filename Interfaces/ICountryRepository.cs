﻿using Microsoft.EntityFrameworkCore.Update.Internal;
using ThePokemonProject.Models;

namespace ThePokemonProject.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromCountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);

    }
}
