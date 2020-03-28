using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.Models
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries { get; }

        Country GetCountryById(string countryId);
    }
}
