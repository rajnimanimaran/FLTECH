using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.purchase_Order
{
    public class PurchaseOrderMasterEntity
    {

        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SupplierID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public int MaterialType { get; set; }
        public string OrderDetails { get; set; }
    }

    public class PurchaseOrderMDEntity
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SupplierID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int MaterialType { get; set; }
        public string OrderDetails { get; set; }


    }



    public class PurchaseOrderDetailsEntity
    {
        public int PurchaseID { get; set; }
        public int ID { get; set; }
        public int MID { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public string GSTNo { get; set; }
        public string HSNCode { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public int Amount { get; set; }
        public int T_Amount { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public int? StatusID { get; set; }
    }


    public class GetPurchaseOrderByID
    {
        public int? PurchaseID { get; set; }
        public int? materialType { get; set; }

    }
    public class purchaseOrderGetEntity
    {

        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SupplierID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
        public int MaterialType { get; set; }
        public int ID { get; set; }
        public int MID { get; set; }
        public int Quantity { get; set; }
        public int UOMID { get; set; }
        public string GSTNo { get; set; }
        public string HSNCode { get; set; }
        public int Rate { get; set; }
        public int Tax { get; set; }
        public int Amount { get; set; }
        public int T_Amount { get; set; }
        public int? StatusID { get; set; }
        public int ActionBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
    public class PurchaseOrders
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public int SID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpectDeliveryDate { get; set; }
    }

    public class GetPurchaseOrderDetails
    {
        public PurchaseOrders PurchaseOrder { get; set; }
        public List<PurchaseOrderDetailsEntity> PurchaseOrderDetailsEntity { get; set; }
    }

}

