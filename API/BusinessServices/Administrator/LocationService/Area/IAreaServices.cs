using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IAreaServices
    {
        AreaEntity GetAreaById(int AreaId);
        IEnumerable<AreaEntity> GetAllAreas();
        IEnumerable<AreaEntity> GetActiveAreaById();
        IEnumerable<AreaEntity> GetAreaByCityId(int CityId);
        ResultDTO CreateArea(AreaEntity AreaEntity);
        ResultDTO UpdateArea(int AreaId, AreaEntity AreaEntity);
        bool DeleteArea(int AreaId);
        bool ToggleActiveAreas(int AreaId);
       
    }
}
