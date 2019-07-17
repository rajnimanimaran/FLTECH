using BusinessEntities.Master1.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.Customer
{
   public interface ICustomerService
    {
        IEnumerable<CustomerEntity> GetAllCustomer();
        bool Create(CustomerEntity obj);
        bool Update(int CustId, CustomerEntity obj);
        bool Delete(int CustId);

    }
}
