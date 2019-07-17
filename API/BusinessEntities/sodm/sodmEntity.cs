using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.sodm
{
   public class sodmgEntity
    {
        public int OrderID { get; set; }
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public string CODE { get; set; }
        public DateTime OrderDate { get; set; }
        public int SID { get; set; }
        public string SName { get; set; }
        public DateTime ACKDate { get; set; }
        public string AssignedTo { get; set; }
        public int? prdID { get; set; }
        public string prdName { get; set; }
        public int Quantity { get; set; }
        public int? UOMID { get; set; }
        public string UOMName { get; set; }
        public decimal? GSTNO { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public double Amount { get; set; }
        public double? T_Amount { get; set; }
    }
    public class sodmsomentity
    {
        public int OrderID { get; set; }
        public int OrderNumber { get; set; }
        public string CODE { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ACKDate { get; set; }
        public string OrderTakenBy { get; set; }
        public string AssignedTo { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
    public class sodmsodEntity
    {
        public int OrderID { get; set; }
        public int ID { get; set; }
        public string OrderItemID { get; set; }
        public string AssignedTo { get; set; }
        public int prdID { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public decimal GSTNO { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public double Amount { get; set; }
        public double T_Amount { get; set; }
        public bool Status { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
    public class sodmsomdEntity
    {
        public int OrderID { get; set; }
        public int OrderNumber { get; set; }
        public string CODE { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ACKDate { get; set; }
        public string OrderTakenBy { get; set; }
        public string AssignedTo { get; set; }
        public int ActionBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string OrderDetails { get; set; }

    }

    public class GetSalesOrderDetails
    {
        public SalesOrders SalesOrder { get; set; }
        public List<SalesOrderDetails>SalesOrderDetails { get; set; }
    }

    public class SalesOrders
    {
        public int OrderID{ get; set; }
        public string CODE { get; set; }
        public int OrderNumber { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ACKDate { get; set; }
    }

    public class SalesOrderDetails
    {
        public int OrderID { get; set; }
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public string CODE { get; set; }
        public DateTime OrderDate { get; set; }
        public int SID { get; set; }
        public string SName { get; set; }
        public DateTime ACKDate { get; set; }
        public string AssignedTo { get; set; }
        public int? prdID { get; set; }
        public string prdName { get; set; }
        public int Quantity { get; set; }
        public int? UOMID { get; set; }
        public string UOMName { get; set; }
        public decimal? GSTNO { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public double Amount { get; set; }
        public double? T_Amount { get; set; }
    }
}

