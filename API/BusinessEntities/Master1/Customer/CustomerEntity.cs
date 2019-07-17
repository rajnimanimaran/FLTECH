using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.Customer
{
   public class CustomerEntity
    {
        public int CustId { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public string CustContact { get; set; }
        public string CustEmail { get; set; }
        public string GSTNo { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustCountry { get; set; }
        public int ActionBy { get; set; }
        public bool IsActive { get; set; }
    }
}
