using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
public interface ISalesOrderService
    {
        bool Create(SalesOrderMaster obj);
        GetSalesDetails GetAllsales(int OrderID);
        bool Update(int OrderID, SalesOrderMaster obj);
        bool Delete(int OrderID, int ActionBy);
        IEnumerable<GetSalesOrderCodeEntity> GetSalesCode();
        IEnumerable<SalesOrderPlanEntity> GetSalesOrderPlan(int SO_RefID);
        bool InsertSalesOrderPlan(SalesOrderPlanEntity obj);
        bool UpdateSalesOrderPlan(int OrderSetID, SalesOrderPlanEntity obj);
    }
}
