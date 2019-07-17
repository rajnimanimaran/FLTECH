using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities

{
    public class WorkOrderEntity
    {
        public int WorkOrderID { get; set; }
        public int OrderID { get; set; }
        public int ContID { get; set; }
        public string AssignedDate { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        public bool IsActive { get; set; }
        public int ActionBy { get; set; }
        public int Status { get; set; }
        public string OrderDetails { get; set; }
        public List<getWOSODetails> getWOSODetails { get; set; }
    }
    public class getWOSODetails
    {
        public int WorkOrderID { get; set; }
        public int WOPRDSerialID { get; set; }
        public int prdID { get; set; }
        public string prdName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }
        public int WOStatus { get; set; }
        public int Quantity { get; set; }
        public int DeliveredQuantity { get; set; }        
        public int WODetlID { get; set; }
        public int BalanceQuantity { get; set; }
        public int ApprovedQuantity { get; set; }
        public int RejectedQuantity { get; set; }
        public int ReworkQuantity { get; set; }
        public string StatusName { get; set; }

    }

    public class getWOSDetails
    {

        public int WOPRDSerialID { get; set; }
        public int WODetlID { get; set; }
        public int WorkOrderID { get; set; }
        public int EmployeeID { get; set; }
        public int WOStatus { get; set; }
        public int AssignTo { get; set; }
        public string StatusName { get; set; }
        public string employeeName { get; set; }

    }
    public class getQCDetails
    {
        public int QCID { get; set; }
        public int ChkListID { get; set; }
        public string chkName { get; set; }
    }

    public class GetOS
    {
        public int OrderStatusID { get; set; }
        public string StatusName { get; set; }
    }

    //public class WorkOrderEntity
    //{
    //    public int WorkOrderID { get; set; }
    //    public int WorkOrderNumber { get; set; }
    //    public int OrderID { get; set; }
    //    public int subContID { get; set; }
    //    public DateTime AssignedDate { get; set; }
    //    public bool Status { get; set; }
    //    public DateTime ExpectedDeliveryDate { get; set; }
    //    public int ActionBy { get; set; }
    //    public bool IsActive { get; set; }
    //    public string WorkOrder_tDetails { get; set; }
    //}
    //public class WorkOrderGetEntity
    //{
    //    public int WorkOrderID { get; set; }
    //    public int WorkOrderNumber { get; set; }
    //    public int OrderID { get; set; }
    //    public int SID { get; set; }
    //    public int prdID { get; set; }
    //    public int UOMID { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal Amount { get; set; }
    //    public int Tax { get; set; }
    //    public int T_Amount { get; set; }
    //}
    //public class GetWorkOrderDetails
    //{
    //    public WorkOrders WorkOrder { get; set; }
    //    public List<WorkOrderDetails> WorkOrderDetails { get; set; }
    //}
    //public class WorkOrders
    //{
    //    public int WorkOrderID { get; set; }
    //    public int WorkOrderNumber { get; set; }
    //    public int OrderID { get; set; }
    //    public int subContID { get; set; }
    //    public DateTime AssignedDate { get; set; }
    //    public bool Status { get; set; }
    //    public DateTime ExpectedDeliveryDate { get; set; }
    //}

    //public class WorkOrderDetails
    //{
    //    public int WorkOrderID { get; set; }
    //    public int WorkOrderNumber { get; set; }
    //    public int OrderID { get; set; }
    //    public int SID { get; set; }
    //    public int prdID { get; set; }
    //    public int UOMID { get; set; }
    //    public int Quantity { get; set; }
    //    public int Tax { get; set; }
    //    public decimal Amount { get; set; }
    //    public decimal? TotalAmount { get; set; }
    //}
    //public class getdropsalesorderID
    //{
    //    public int OrderID { get; set; }
    //}



    //public class getWOSODetailsgtab
    //{
    //    public getWOSODetails WorkOrdersod { get; set; }
    //}

}
