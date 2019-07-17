using BusinessEntities.Master1.StockRawMaterial;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.StockRawMaterial
{
  public  class rawmaterialStockServices: Irmstockservice
    {
        private readonly IUnitOfWork _unitOfWork;

        public rawmaterialStockServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<rawMaterialStockEntity> GetAllRmSk()
        {
            SqlCommand cmd = new SqlCommand("RMST_spGetRawMaterialStock");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<rawMaterialStockEntity>(cmd);
            return locMas;
        }
        public bool CreateRmSk(rawMaterialStockEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RMST_spSaveRawMaterialStock");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
           // cmd.Parameters.AddWithValue("@p_RMName", obj.RMName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateRmSk(rawMaterialStockEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RMST_spSaveRawMaterialStock");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_stkRMID", obj.stkRMID);
            cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
           // cmd.Parameters.AddWithValue("@p_RMName", obj.RMName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeleteRmSk(int stkRMID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("RMST_spRemoveRawMaterialStock");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_stkRMID", stkRMID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }

        public IEnumerable<getrawMaterialName> GetAllRmSks()
        {
            SqlCommand cmd = new SqlCommand("RM_spGetRawMaterialName");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<getrawMaterialName>(cmd);
            return locMas;
        }
    }
}
