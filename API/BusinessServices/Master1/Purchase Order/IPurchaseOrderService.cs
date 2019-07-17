

using BusinessEntities;
using BusinessEntities.Master1.SalesOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IPurchaseOrderService
    {
        IEnumerable<PurchaseGetEntity> GetAllsodm();
        bool Create(PurchaseOrderCommonEntity obj);
        bool Update(int PurchaseID, PurchaseOrderCommonEntity obj);
        bool Delete(int ID,int PurchaseID);


        IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName();
        GetPurchaseOrderDetails GetPurchaseOrderDetails(int PurchaseID);
        IEnumerable<SalesGetEntity> GetAllpodm();
        bool Create(SalesOrderCommonEntity obj);
         bool Update(int OrderID, SalesOrderCommonEntity obj);
        // bool Delete(int ID, int SalesID);
        //IEnumerable<getSuppliername> GetAllSupplierName();
        GetSalesOrderDetails GetSalesOrderDetails(int OrderID);
   
    }
}
