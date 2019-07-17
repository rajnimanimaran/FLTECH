using BusinessEntities.Master1.RawMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.RawMaterial
{
   public interface IRawMaterialService
    {
        IEnumerable<RawMaterialMasterEntity> GetAllRawmaterial();
        bool CreateRawmaterial(RawMaterialMasterEntity obj);
        bool UpdateRawmaterial(int RMID, RawMaterialMasterEntity obj);
        bool DeleteRawmaterial(int RMID);
    }
}
