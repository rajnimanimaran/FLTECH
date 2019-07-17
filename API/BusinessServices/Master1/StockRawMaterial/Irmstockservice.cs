using BusinessEntities.Master1.StockRawMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.StockRawMaterial
{
   public interface Irmstockservice
    {
        IEnumerable<rawMaterialStockEntity> GetAllRmSk();
        bool CreateRmSk(rawMaterialStockEntity obj);
        bool UpdateRmSk(rawMaterialStockEntity obj);
        bool DeleteRmSk(int RMID);
        IEnumerable<getrawMaterialName> GetAllRmSks();
    }
}
