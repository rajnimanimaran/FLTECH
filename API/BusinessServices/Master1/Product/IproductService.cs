using BusinessEntities;
using BusinessEntities.Master1.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.Product
{
   public interface IproductService
    {
        IEnumerable<productEntity> GetAllproduct();
        bool Createproduct(productEntity obj);
        bool Updateproduct(int prdID, productEntity obj);
        bool Deleteproduct(int prdID);
        IEnumerable<ProductMasterEntity> GetProductMaster(int prdID);
        IEnumerable<UOMMasterEntity> GetUOMDetails();
    }
}
