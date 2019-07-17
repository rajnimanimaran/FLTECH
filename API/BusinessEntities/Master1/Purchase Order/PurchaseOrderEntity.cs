using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class PurchaseGetEntity
    {
        public int PurchaseID { get; set; }
        public int ID { get; set; }
        public string code { get; set; }
        public int? SID { get; set; }
        public string SName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public int? RMID { get; set; }
        public string RMName { get; set; }
        public int? Quantity { get; set; }
        public int? UOMID { get; set; }
        public string UOMName { get; set; }
        public double GST { get; set; }
        public int Rate { get; set; }
        public int? MaterialType { get; set; }
        public int Tax { get; set; }
        public double Amount { get; set; }
        public double? T_Amount { get; set; }


    }
    public class GetPurchaseDetails
    {
        public List<PurchaseGetEntity> PurchaseOrderMaster;
        public List<PurchaseOrderDetails> PurchaseOrderDetails;
    }

    public class ResultPurchase
    {
        public DataSet data { get; set; }
        public string path { get; set; }
    }

    public class PurchaseGetReportEntity
    {
        public int PurchaseID { get; set; }
        public int ID { get; set; }
        public string code { get; set; }
        public int? SID { get; set; }
        public string SName { get; set; }
        public int? SGSTNo { get; set; }
        public string SPhNo { get; set; }
        public string SState { get; set; }
        public int? SStateCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public int? RMID { get; set; }
        public string RMName { get; set; }
        public int? Quantity { get; set; }
        public string UOMCode { get; set; }
        public string UOMName { get; set; }
        public double GST { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public double Amount { get; set; }
        public double? T_Amount { get; set; }
        public DataTable PurchaseReport { get; set;}

    }

    public class PurchaseOrderCommonEntity
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SupplierID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string TransportMode { get; set; }
        public string VechicleNumber { get; set; }
        public DateTime DateOfSupply { get; set; }
        public string PlaceOfSupply { get; set; }
        public int ActionBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string OrderDetails { get; set; }
    }
    public class PurchaseMDEntity
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SupplierID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public int ActionBy { get; set; }
        public int RMID { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public float GST { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public int Amount { get; set; }
        public decimal T_Amount { get; set; }
        public int ID { get; set; }
        public byte IsActive { get; set; }
        public string SName { get; set; }

    }
    public class getSuppliername
    {
        public int SID { get; set; }
        public string SName { get; set; }
    }

    public class GetPurchaseOrderDetails
    {
        public PurchaseOrders PurchaseOrder { get; set; }
        public List<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
    }

    public class PurchaseOrders
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
    }

    public class PurchaseOrderDetails
    {
        public int PurchaseID { get; set; }
        public int? RMID { get; set; }
        public int? MID { get; set; }
        public int ID { get; set; }
        public int? prdID { get; set; }
        public string prdName { get; set; }
        public string HSNCode { get; set; }
        public string RMName { get; set; }
        public int UOMID { get; set; }
        public string UOMName { get; set; }
        public int Quantity { get; set; }
        public int? GSTNo { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal? T_Amount { get; set; }
    }
    public class GetReportPurchase
    {
        public int PurchaseID { get; set; }
    }

}
