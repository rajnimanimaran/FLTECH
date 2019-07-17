using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.WorkOrder
{
   public interface IWorkOrderService 
    {
        WorkOrderEntity Create(WorkOrderEntity obj);
        IEnumerable<getWOSODetails> GetWorkOrdersoDetails(int WorkOrderID);
        IEnumerable<getWOSDetails> WOSdetails(int WODetlID);
        IEnumerable<getQCDetails> QCdetails(int PrdID);
        IEnumerable<GetOS> GetOSList(int OrderStatusID);
        //IEnumerable<WorkOrderGetEntity> GetAllsodm();
        //bool Create(WorkOrderEntity obj);
        //bool Update(int WorkOrderID, WorkOrderEntity obj);
        ////bool Delete(int ID, int PurchaseID);
        ////IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName();
        ////IEnumerable<getdropsalesorder> Getsalesorderdrop();
        //IEnumerable<getdropsalesorderID> Getsalesorderdrop();
        //GetWorkOrderDetails GetWorkOrderDetails(int WorkOrderID);
        //IEnumerable<getWOSODetails> GetWorkOrdersoDetails(int OrderID);

    }
}
