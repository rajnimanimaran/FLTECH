using BusinessEntities.BOM_Details;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.BOM_Details
{
   public class BOM_DetailsServices : IBOM_DetailsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public BOM_DetailsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BOM_DetailsEntity> GetAllBOM_Details()
        {
            SqlCommand cmd = new SqlCommand("BOM_spGetBOM_Details");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<BOM_DetailsEntity>(cmd);
            return locMas;
        }
        public bool CreateBOM_Details(BOM_DetailsEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spSaveBOM_Details");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_BOMD_ID", obj.BOMD_ID);
            cmd.Parameters.AddWithValue("@p_BOMID", obj.BOMID);
            cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
            cmd.Parameters.AddWithValue("@p_quantity", obj.quantity);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateBOM_Details(int BOMD_ID, BOM_DetailsEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spSaveBOM_Details");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_BOMD_ID", obj.BOMD_ID);
            cmd.Parameters.AddWithValue("@p_BOMID", obj.BOMID);
            cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
            cmd.Parameters.AddWithValue("@p_quantity", obj.quantity);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeleteBOM_Details(int BOMD_ID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spRemoveBOM_Details");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_BOMD_ID", BOMD_ID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
    }
}
