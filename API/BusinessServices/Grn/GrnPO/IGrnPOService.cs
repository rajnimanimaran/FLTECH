using BusinessEntities.Grn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Grn.GrnPO
{
    public interface IGrnPOService
    {
        //bool Create(GrnEntity obj);
        bool SaveGRNPO(SaveGRNEntity obj);
        bool Update(GrnPOUpdate obj);
        bool Delete(int GrnNo, int ActionBy);
        IEnumerable<GrnEntity> select(int? GrnNo);
        IEnumerable<GrnEntity> GetAllGrn();
        IEnumerable<getpo> GetAllRM();
        IEnumerable<getpo> GetAllPrd();


    }
}
