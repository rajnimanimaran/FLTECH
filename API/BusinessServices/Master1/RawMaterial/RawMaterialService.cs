using BusinessEntities;
using BusinessEntities.Master1.RawMaterial;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.RawMaterial
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RawMaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<RawMaterialMasterEntity> GetAllRawmaterial()
        {
            SqlCommand cmd = new SqlCommand("RM_spGetRawMaterial");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<RawMaterialMasterEntity>(cmd);
            return locMas;
        }
        public bool CreateRawmaterial(RawMaterialMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RM_spSaveRawMaterial");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_RMCode", obj.RMCode);
            cmd.Parameters.AddWithValue("@p_RMName", obj.RMName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_splitable", obj.splitable);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            //cmd.Parameters.AddWithValue("@p_HSNCode", obj.HSNCode);
            cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            //cmd.Parameters.AddWithValue("@createdOn", obj.createdOn);
            //cmd.Parameters.AddWithValue("@modifiedOn", obj.modifiedOn);
            //cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateRawmaterial(int RMID, RawMaterialMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RM_spSaveRawMaterial");
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
            cmd.Parameters.AddWithValue("@p_RMCode", obj.RMCode);
            cmd.Parameters.AddWithValue("@p_RMName", obj.RMName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_splitable", obj.splitable);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            //cmd.Parameters.AddWithValue("@p_HSNCode", obj.HSNCode);
            cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            // cmd.Parameters.AddWithValue("@createdOn", obj.createdOn);
            //cmd.Parameters.AddWithValue("@modifiedOn", obj.modifiedOn);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeleteRawmaterial(int RMID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RM_spRemoveRawMaterial");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_RMID", RMID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }


    }


}


