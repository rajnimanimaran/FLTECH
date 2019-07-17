using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IActionServices
    {        
        ActionEntity GetActionById(int ActionId);
        IEnumerable<ActionEntity> GetAllActions();
        int CreateAction(ActionEntity actionEntity);
        bool UpdateAction(int ActionId, ActionEntity actionEntity);
        bool DeleteAction(int ActionId);
    }
}

