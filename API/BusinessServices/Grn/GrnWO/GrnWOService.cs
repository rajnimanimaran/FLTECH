using BusinessEntities.Grn;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Grn.GrnWO
{
    public class GrnWOService : IGrnWOService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GrnWOService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Create(GrnWOEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("GRN_spSaveGRNWorkOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@p_GrnNo", obj.GrnNo);
            cmd.Parameters.AddWithValue("@p_WorkOrderID", obj.WorkOrderID);
            cmd.Parameters.AddWithValue("@p_InvoiceNo", obj.InvoiceNo);
            cmd.Parameters.AddWithValue("@p_InvoiceDate", obj.InvoiceDate);
            cmd.Parameters.AddWithValue("@p_Remarks", obj.Remarks);
            //cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            //cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
            cmd.Parameters.AddWithValue("@p_WODetails", obj.WODetails);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool Update(GrnWOUpdate obj)
        {
            {
                bool res = false;
                SqlCommand cmd = new SqlCommand("BOM_spGRNStatusUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_GrnNo", obj.GrnNo);
                //cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
                cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
                cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
                cmd.Parameters.AddWithValue("@p_WODetails", obj.WODetails);
                //cmd.Parameters.AddWithValue("@p_ModifiedBy", obj.ModifiedBy);
                //cmd.Parameters.AddWithValue("@p_CreatedOn", obj.CreatedOn);
                //cmd.Parameters.AddWithValue("@p_GrnDetails", obj.GrnDetails);
                var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
                if (locMax != Int32.MaxValue)
                {
                    res = true;
                }
                return res;
            }
        }
        public bool Delete(int GrnNo, int ActionBy)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spRemoveGRNDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_GrnNo", GrnNo);
            cmd.Parameters.AddWithValue("@P_ActionBy", ActionBy);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public IEnumerable<GrnWOEntity> select(int? GrnNo)
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchGRNDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_GrnNo", GrnNo);
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnWOEntity>(cmd);
            return locMas;
        }
        public IEnumerable<GrnWOEntity> GetAllGrnwo()
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchGRNWO");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnWOEntity>(cmd);
            return locMas;


        }

        public IEnumerable<GrnWOEntity> GetProductList()
        {
            SqlCommand cmd = new SqlCommand("Sp_GetGrnWOPrd");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnWOEntity>(cmd);
            return locMas;
        }

        public IEnumerable<GrnWOID> GetWOList(int WorkOrderID)
        {
            SqlCommand cmd = new SqlCommand("sp_getWOID");
            cmd.Parameters.AddWithValue("@p_WorkOrderID", WorkOrderID);
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnWOID>(cmd);
            return locMas;
        }


    }
}
