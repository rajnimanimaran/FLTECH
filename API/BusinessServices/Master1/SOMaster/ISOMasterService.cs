using BusinessEntities.Master1.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.SOMaster
{
   public interface ISOMasterService
    {
        IEnumerable<SalesGetEntity> GetAllpodm();
        bool Create(SalesOrderCommonEntity obj);
        // bool Update(int SalesID, SalesOrderCommonEntity obj);
        // bool Delete(int ID, int SalesID);
        //IEnumerable<getSuppliername> GetAllSupplierName();
        GetSalesOrderDetails GetSalesOrderDetails(int OrderID);
    }
}
