using BusinessEntities.Master1.UOM;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.UOM
{
    public class UOMMasterService : IUOMMasterService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UOMMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UOMMasterEntity> GetAllUOM()
        {
            SqlCommand cmd = new SqlCommand("UOM_spgetUOM");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<UOMMasterEntity>(cmd);
            return locMas;
        }
        public bool CreateUOM(UOMMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("UOM_spSaveUOM");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_UOMCode", obj.UOMCode);
            cmd.Parameters.AddWithValue("@p_UOMName", obj.UOMName);
            cmd.Parameters.AddWithValue("@p_Multifier", obj.Multifier);
            cmd.Parameters.AddWithValue("@p_BaseUnit", obj.BaseUnit);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateUOM(int RMID, UOMMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("UOM_spSaveUOM");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_UOMCode", obj.UOMCode);
            cmd.Parameters.AddWithValue("@p_UOMName", obj.UOMName);
            cmd.Parameters.AddWithValue("@p_Multifier", obj.Multifier);
            cmd.Parameters.AddWithValue("@p_BaseUnit", obj.BaseUnit);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeleteUOM(int UOMID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("UOM_spremoveUOM");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_UOMID", UOMID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
    }
}
