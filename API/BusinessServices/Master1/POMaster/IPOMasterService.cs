using BusinessEntities;
using BusinessEntities.Master1.SalesOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.POMaster
{
    public interface IPOMasterService
    {
        IEnumerable<PurchaseGetEntity> GetAllsodm();
        IEnumerable<GetReportPurchase> Getreportpurchase();
        IEnumerable<SalesGetReportID> SalesGetReportID();
        bool Create(PurchaseOrderCommonEntity obj);
        bool Update(int PurchaseID, PurchaseOrderCommonEntity obj);
        bool Delete(int PurchaseID);
        bool DeleteS(int OrderID);
        IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName();
        GetPurchaseOrderDetails GetPurchaseOrderDetails(int PurchaseID);
        IEnumerable<SalesGetEntity> GetAllpodm();
        bool Create(SalesOrderCommonEntity obj);
        // bool Update(int SalesID, SalesOrderCommonEntity obj);
        // bool Delete(int ID, int SalesID);
        //IEnumerable<getSuppliername> GetAllSupplierName();
        GetSalesOrderDetails GetSalesOrderDetails(int OrderID);
        bool Update(int OrderID, SalesOrderCommonEntity obj);
        DataSet ReturnPurchaseList(int PurchaseID,PurchaseGetReportEntity obj);
        DataSet ReturnSalesList(int OrderID, SalesGetReportEntity obj);
    }
}
