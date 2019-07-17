using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Grn
{
    public class GrnEntity
    {
        public int GrnNo { get; set; }
        public int PurchaseID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public int StatusID { get; set; }
        //public int CreatedBy { get; set; }
        //public int ModifiedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public int MaterialType { get; set; }
        public int ActionBy { get; set; }
        public string PODetails { get; set; }
    }
    


    public class SaveGRNEntity
    {
        public int PurchaseID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int ActionBy { get; set; }
        public int MaterialType { get; set; }
        public string PODetails { get; set; }


    }


    public class GrnPOUpdate
    {
        public int GrnNo { get; set; }
        public int StatusID { get; set; }
        public int MaterialType { get; set; }
        public int ActionBy { get; set; }
        public string PODetails { get; set; }

    }
    public class GrnWOEntity
    {
        public int GrnNo { get; set; }
        
        public int WorkOrderID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public string prdName { get; set; }
        public string UOMName { get; set; }
        public int UOMID { get; set; }
        public int PrdID { get; set; }
        public int StatusID { get; set; }
        public bool IsActive { get; set; }
        public int ActionBy { get; set; }
        public string WODetails { get; set; }
    }
    public class GrnWOID
    {
        public int WorkOrderID { get;   set; }
        public int PrdID { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public int ProductID { get; set; }
        public string prdName { get; set; }
        public int WOPRDSerialID { get; set; }
        public string UOMName { get; set; }
        

    }
    public class GrnWOUpdate
    {
        public int GrnNo { get; set; }
        public int MaterialType { get; set; }
        public int ActionBy { get; set; }
        public string WODetails { get; set; }

    }

    public class getpo
    {
        public int GrnNo { get; set; }
        public int PurchaseID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Remarks { get; set; }
        public int StatusID { get; set; }
        public int RMID { get; set; }
        public string RMName { get; set; }
        public string UOMName { get; set; }
         public int UOMID { get; set; }
        public int PrdID { get; set; }
        public string prdName { get; set; }
    }


}

