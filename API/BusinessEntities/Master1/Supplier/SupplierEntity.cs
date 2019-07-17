using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Supplier
{
  public  class SupplierEntity
    {
        public int SID { get; set; }
        public string SName { get; set; }
        public string SPhNo { get; set; }
        public int SGSTNo { get; set; }
        public string SState { get; set; }
        public string SStateCode { get; set; }
        public string SPan { get; set; }
        public string SAddress { get; set; }
    }
}
