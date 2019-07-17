using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ICountryService
    {
        CountryEntity GetCountryById(int CountryId);
        IEnumerable<CountryEntity> GetAllCountry();
        IEnumerable<CountryEntity> GetActiveCountryById();
        ResultDTO CreateCountry(CountryEntity CountryEntity);
        ResultDTO UpdateCountry(int CountryId, CountryEntity CountryEntity);
        bool DeleteCountry(int CountryId);
        bool ToggleActiveCountry(int CountryId);
      


        //long CreateCountry(CountryEntity Country);
    }
}
