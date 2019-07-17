using BusinessEntities.Master1.UOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.UOM
{
   public interface IUOMMasterService
    {
        IEnumerable<UOMMasterEntity> GetAllUOM();
        bool CreateUOM(UOMMasterEntity obj);
        bool UpdateUOM(int UOMID, UOMMasterEntity obj);
        bool DeleteUOM(int UOMID);
    }
}

