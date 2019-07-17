using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.StockRawMaterial
{
    public class rawMaterialStockEntity
    {
        public int stkRMID { get; set; }
        public int RMID { get; set; }
        public string RMName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }
        public int stock { get; set; }
        public int re_Orderlevel { get; set; }
        public int ActionBy { get; set; }
        public byte IsActive { get; set; }

    }

    public class getrawMaterialName
    {

        public int RMID { get; set; }
        public string RMName { get; set; }
    }
}

