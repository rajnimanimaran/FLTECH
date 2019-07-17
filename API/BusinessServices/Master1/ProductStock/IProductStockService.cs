using BusinessEntities.Master1.ProductStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.ProductStock
{
    public interface IProductStockService
    {
        IEnumerable<ProductStockEntity> GetAllPrdSk();
        bool CreatePrdSk(ProductStockEntity obj);
        bool UpdatePrdSk(int StockID, ProductStockEntity obj);
        bool DeletePrdSk(int StockID);
    }
}
