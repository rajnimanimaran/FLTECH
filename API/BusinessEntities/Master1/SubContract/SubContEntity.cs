using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.SubContract
{
  public  class SubContEntity
    {
        public int subContID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string GstNo { get; set; }
        public string contactNumber { get; set; }
        public int supplierID { get; set; }
        public string emailID { get; set; }
        public int ActionBy { get; set; }
        public bool IsActive { get; set; }

    }
}
