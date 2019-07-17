using BusinessEntities.Master1.Customer;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.Customer
{
   public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<CustomerEntity> GetAllCustomer()
        {
            SqlCommand cmd = new SqlCommand("[dbo].[Cust_spSelectCustomer]");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<CustomerEntity>(cmd);

            return locMas;

        }




        public bool Create(CustomerEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("[dbo].[Cust_SpSaveCustmor]");
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@p_CustId", obj.CustId);
            cmd.Parameters.AddWithValue("@p_CustCode", obj.CustCode);
            cmd.Parameters.AddWithValue("@p_CustName", obj.CustName);
            cmd.Parameters.AddWithValue("@p_CustAddress", obj.CustAddress);
            cmd.Parameters.AddWithValue("@p_CustContact", obj.CustContact);
            cmd.Parameters.AddWithValue("@p_CustEmail", obj.CustEmail);
            cmd.Parameters.AddWithValue("@p_GSTNo", obj.GSTNo);
            cmd.Parameters.AddWithValue("@p_CustCity", obj.CustCity);
            cmd.Parameters.AddWithValue("@p_CustState", obj.CustState);
            cmd.Parameters.AddWithValue("@p_CustCountry", obj.CustCountry);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }



        public bool Update(int CustId, CustomerEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("[dbo].[Cust_SpSaveCustmor]");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_CustId", obj.CustId);
            cmd.Parameters.AddWithValue("@p_CustCode", obj.CustCode);
            cmd.Parameters.AddWithValue("@p_CustName", obj.CustName);
            cmd.Parameters.AddWithValue("@p_CustAddress", obj.CustAddress);
            cmd.Parameters.AddWithValue("@p_CustContact", obj.CustContact);
            cmd.Parameters.AddWithValue("@p_CustEmail", obj.CustEmail);
            cmd.Parameters.AddWithValue("@p_GSTNo", obj.GSTNo);
            cmd.Parameters.AddWithValue("@p_CustCity", obj.CustCity);
            cmd.Parameters.AddWithValue("@p_CustState", obj.CustState);
            cmd.Parameters.AddWithValue("@p_CustCountry", obj.CustCountry);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }


        public bool Delete(int CustId)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("[dbo].[Cust_SpRemoveCustomer]");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_CustId", CustId);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
    }

}

