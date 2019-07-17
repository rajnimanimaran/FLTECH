using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IStateServices
    {
        StateEntity GetStateById(int StateId);
        IEnumerable<StateEntity> GetAllStates();
        IEnumerable<StateEntity> GetActiveStateById();
        ResultDTO CreateState(StateEntity StateEntity);
        ResultDTO UpdateState(int StateId, StateEntity StateEntity);
        bool DeleteState(int StateId);
        IEnumerable<StateEntity> GetStateByCountryId(int CountryId);
        bool ToggleActiveState(int StateId);
    }
}
