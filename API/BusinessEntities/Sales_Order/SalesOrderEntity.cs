using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
 public class SalesOrderMaster
    {
        public int OrderID { get; set; }
       // public string CODE { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ACKDate { get; set; }
        public int AssignedTo { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; }
        public int OrderTakenBy { get; set; }
        public int OrderStatus { get; set; }
        public int OrderStatusID { get; set; }
        public string OrderDetails { get; set; }
        public string StatusName { get; set; }
        public string employeeName { get; set; }
        public string AssignedemployeeName { get; set; }
    }


    public class SalesOrderDetails
    {
        public int OrderID { get; set; }
        public bool Planned { get; set; }
        //public string CODE { get; set; }
        //public DateTime OrderDate { get; set; }
       // public DateTime ACKDate { get; set; }
        //public int AssignedTo { get; set; }
        //public bool IsActive { get; set; }
        //public string CustName { get; set; }
       // public int OrderTakenBy { get; set; }
        //public int OrderStatus { get; set; }
        public int DetlID { get; set; }
        public int OrderItemID { get; set; }
        public string prdName { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public string HSNCode { get; set; }
        public decimal Rate { get; set; }
        public int Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal T_Amount { get; set; }
        public int AssignedQty { get; set; }
        public int AlreadyAssignedQty { get; set; }
        public int BalanceQty { get; set; }
        public int WorkStatus { get; set; }
        public int prdID { get; set; }
        public string UOMName { get; set; }
        public int sell_price { get; set; }
        public int cost_price { get; set; }
        public string StatusName { get; set; }
        public string employeeName { get; set; }

    }

    public class GetSalesDetails
    {
        public List<SalesOrderMaster> SalesOrderMaster;
        public List<SalesOrderDetails> SalesOrderDetails;
        public List<SalesOrderPlanEntity> SalesOrderPlanEntity;

    }


    public class SalesOrderDetailsEntity

    {
        public int OrderID { get; set; }
       public string CODE { get; set; }
        public DateTime OrderDate { get; set; }
         public DateTime ACKDate { get; set; }
        public int AssignedTo { get; set; }
        public bool IsActive { get; set; }
        public string CustName { get; set; }
         public int OrderTakenBy { get; set; }
        public int OrderStatus { get; set; }
        public int DetlID { get; set; }
        public int OrderItemID { get; set; }
        public string prdName { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public string HSNCode { get; set; }
        public decimal Rate { get; set; }
        public int Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal T_Amount { get; set; }
        public int AssignedQty { get; set; }
        public int AlreadyAssignedQty { get; set; }
        public int BalanceQty { get; set; }
        public int WorkStatus { get; set; }
        public int prdID { get; set; }
        public string UOMName { get; set; }

    }

    public class SalesOrderPlanEntity

    {
        public int OrderSetID { get; set; }
        public int SOID { get; set; }
        public int SORefID { get; set; }
        public int Quantity { get; set; }
        public DateTime Deliverydate { get; set; }
        public DateTime OrdAckSdate { get; set; }
        public DateTime OrdAckEdate { get; set; }
        public DateTime MatRcvInFacSdate { get; set; }
        public DateTime MatRcvInFacEdate { get; set; }
        public DateTime MatInFlowSdate { get; set; }
        public DateTime MatInFlowEdate { get; set; }
        public DateTime AssembleSdate { get; set; }
        public DateTime AssembleEdate { get; set; }
        public DateTime HydrolicSdate { get; set; }
        public DateTime HydrolicEdate { get; set; }
        public DateTime PerfTestSdate { get; set; }
        public DateTime PerfTestEdate { get; set; }
        public DateTime PaintingSdate { get; set; }
        public DateTime PaintingEdate { get; set; }
        public DateTime FinalInspSdate { get; set; }
        public DateTime FinalInspEdate { get; set; }
        public DateTime PackingSdate { get; set; }
        public DateTime PackingEdate { get; set; }

    }


    public class GetSalesOrderCodeEntity

    {
        public int OrderID { get; set; }
        public string CODE { get; set; }

    }


    }
