using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.RawMaterial
{
    public class RawMaterialMasterEntity
    {
        public int RMID { get; set; }
        public string RMCode { get; set; }
        public string RMName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }
        public bool splitable { get; set; }
        public int stock { get; set; }
        public int re_Orderlevel { get; set; }
        //public string HSNCode { get; set; }
        public float sell_price { get; set; }
        public float cost_price { get; set; }
        public int ActionBy { get; set; }
        public byte IsActive { get; set; }
    }
}
