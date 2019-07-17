using BusinessEntities.Supplier;
using System.Collections.Generic;
//using SupplierEntity = BusinessEntities.Supplier.SupplierEntity;

namespace BusinessServices.Supplier
{
    public interface ISupplierservice
    {
        IEnumerable<SupplierEntity> GetSupplier();
        bool CreateSupplier(SupplierEntity obj);
        bool UpdateSupplier(int SID, SupplierEntity obj);
        bool DeleteSupplier(int SID);
    }
}
