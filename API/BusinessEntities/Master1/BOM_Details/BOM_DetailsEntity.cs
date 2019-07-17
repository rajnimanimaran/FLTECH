using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.BOM_Details
{
   public  class BOM_DetailsEntity
    {
        public int BOMD_ID { get; set; }
        public int BOMID { get; set; }
        public int RMID { get; set; }
        public int quantity { get; set; }
        public int UOMID { get; set; }
        public int ActionBy { get; set; }
        public bool IsActive { get; set; }
    }
}
