using BusinessEntities;
using BusinessEntities.purchase_Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.purchase_Order
{
 public interface IPurchaseOrderMasterService
    {
        GetPurchaseDetails GetAllPurchaseDetails(int PurchaseID, int MaterialType);
        GetPurchaseDetails GetPurchaseListByID(int PurchaseID, int ActionBy);
        IEnumerable<PurchaseOrderMasterEntity> GetActivePurchaseList();
        
        //IEnumerable<purchaseOrderGetEntity> GetAllpurchase();
        bool Create(PurchaseOrderMasterEntity obj);
        bool Update(int PurchaseID, PurchaseOrderMasterEntity obj);
        bool Delete(int PurchaseID, int ActionBy);
        //IEnumerable<GetPurchaseOrderByID> GetAllpurchase(GetPurchaseOrderByID obj);
        IEnumerable<PurchaseOrderMDEntity> get(GetPurchaseOrderByID obj);

    }

}
