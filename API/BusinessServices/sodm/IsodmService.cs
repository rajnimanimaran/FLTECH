using BusinessEntities.sodm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.sodm
{
    public interface IsodmService
    {
        IEnumerable<sodmgEntity> GetAllsodm();
       // IEnumerable<sodmsodEntity> GetStateByCountryId(int CountryId);
        bool Create(sodmsomdEntity obj);
    }
}
