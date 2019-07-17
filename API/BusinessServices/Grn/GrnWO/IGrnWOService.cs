using BusinessEntities.Grn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Grn.GrnWO
{
    public interface IGrnWOService
    {
        bool Create(GrnWOEntity obj);
        bool Update(GrnWOUpdate obj);
        bool Delete(int GrnNo, int ActionBy);
        IEnumerable<GrnWOEntity> select(int? GrnNo);
        IEnumerable<GrnWOEntity> GetAllGrnwo();
        IEnumerable<GrnWOEntity> GetProductList();
        IEnumerable<GrnWOID> GetWOList(int WorkOrderID);
    }
}
