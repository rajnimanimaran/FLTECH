using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IRawMaterialServices
    {
        //RawMaterialEntity GetRawMaterialById(int RawMaterialId);
        IEnumerable<RawMaterialEntity> GetAllRawMaterials();
        bool CreateRawMaterial(RawMaterialEntity obj);
        bool UpdateRawMaterial(int RawMaterialId, RawMaterialEntity obj);
        bool DeleteRawMaterial(int RawMaterialId);
    }
}
