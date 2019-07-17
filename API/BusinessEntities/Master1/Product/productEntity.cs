using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Master1.product
{
   public class productEntity
    {
        public int prdID { get; set; }
        public string prdCode { get; set; }
        public string prdName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }
        public bool splitable { get; set; }
        public int stock { get; set; }
        public int re_Orderlevel { get; set; }
        public string HSNCode { get; set; }
        public float sell_price { get; set; }
        public float cost_price { get; set; }
        public int ActionBy { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public byte IsActive { get; set; }
    }

    public class UOMMasterEntity
    {
        public int UOMID { get; set; }
        public string UOMName { get; set; }
    }

    public class ProductMasterEntity
    {
        public int prdID { get; set; }
        public string prdName { get; set; }
        public string HSNCode { get; set; }
        public string UOMID { get; set; }
        public string UOMName { get; set; }
    }
}
