using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface ICityService
    {
        CityEntity GetCityById(int cityId);
        IEnumerable<CityEntity> GetCityByStateId(int StateId);
        IEnumerable<CityEntity> GetAllCity();
        ResultDTO CreateCity(CityEntity CityEntity);
        ResultDTO UpdateCity(int CityId, CityEntity CityEntity);
        bool DeleteCity(int CityId);
        bool ToggleActiveCity(int CityId);
        IEnumerable<CityEntity> GetActiveCityById();
    }
}
