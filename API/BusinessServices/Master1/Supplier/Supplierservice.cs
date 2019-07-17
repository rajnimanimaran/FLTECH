using BusinessEntities;
using BusinessEntities.Supplier;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Supplier
{
   public class Supplierservice : ISupplierservice
    {
        private readonly IUnitOfWork _unitOfWork;

        public Supplierservice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<BusinessEntities.Supplier.SupplierEntity> GetSupplier()
        {
            SqlCommand cmd = new SqlCommand("sp_SelectSupplier");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<BusinessEntities.Supplier.SupplierEntity>(cmd);
            return locMas;


        }
        public bool CreateSupplier(BusinessEntities.Supplier.SupplierEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_SaveSupplier");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_SID", obj.SID);
            cmd.Parameters.AddWithValue("@p_SName", obj.SName);
            cmd.Parameters.AddWithValue("@p_SPhNo", obj.SPhNo);
            cmd.Parameters.AddWithValue("@p_SGSTNo", obj.SGSTNo);
            cmd.Parameters.AddWithValue("@p_SState", obj.SState);
            cmd.Parameters.AddWithValue("@p_SStateCode", obj.SStateCode);
            cmd.Parameters.AddWithValue("@p_SAddress", obj.SAddress);
            cmd.Parameters.AddWithValue("@p_SPan", obj.SPan);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateSupplier(int SID, BusinessEntities.Supplier.SupplierEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_SaveSupplier");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_SID", obj.SID);
            cmd.Parameters.AddWithValue("@p_SName", obj.SName);
            cmd.Parameters.AddWithValue("@p_SPhNo", obj.SPhNo);
            cmd.Parameters.AddWithValue("@p_SGSTNo", obj.SGSTNo);
            cmd.Parameters.AddWithValue("@p_SState", obj.SState);
            cmd.Parameters.AddWithValue("@p_SStateCode", obj.SStateCode);
            cmd.Parameters.AddWithValue("@p_SAddress", obj.SAddress);
            cmd.Parameters.AddWithValue("@p_SPan", obj.SPan);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeleteSupplier(int SID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("[dbo].[sp_DeleteSupplier]");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_SID", SID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }

        
    }
}
