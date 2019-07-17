using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class StoreRawEntity
    {
        public int StoreId { get; set; }
        public int RawMaterialId { get; set; }
        public string RawMaterialName { get; set; }
        public int UOMId { get; set; }
        public string UOMName { get; set; }
        public string UOMCode { get; set; }
        public string BaseUnits { get; set; }
        public int Quantity { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
