using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.UOM
{
     public class UOMMasterEntity
    {
        public int UOMID { get; set; }
        public string UOMCode { get; set; }
        public string UOMName { get; set; }
        public int Multifier { get; set; }
        public string BaseUnit { get; set; }
        public int ActionBy { get; set; }
        public byte IsActive { get; set; }

    }
}
