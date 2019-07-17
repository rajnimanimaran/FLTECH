using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.ProductStock
{
   public class ProductStockEntity
    {
        public int StockID { get; set; }
        public int prdID { get; set; }
        public string prdName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }

        public int stock { get; set; }
        public int re_Orderlevel { get; set; }
        public int ActionBy { get; set; }
        public byte IsActive { get; set; }
    }
}
