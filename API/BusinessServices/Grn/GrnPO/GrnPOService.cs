using BusinessEntities.Grn;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Grn.GrnPO
{
   public class GrnPOService : IGrnPOService
    {

        private readonly IUnitOfWork _unitOfWork;

        public GrnPOService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public bool Create(GrnEntity obj)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("GRN_spSaveGRNPO");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    //cmd.Parameters.AddWithValue("@p_GrnNo", obj.GrnNo);
        //    cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
        //    cmd.Parameters.AddWithValue("@p_InvoiceNo", obj.InvoiceNo);
        //    cmd.Parameters.AddWithValue("@p_InvoiceDate", obj.InvoiceDate);
        //    cmd.Parameters.AddWithValue("@p_Remarks", obj.Remarks);
        //    //cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
        //    cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
        //    cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
        //    cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
        //    cmd.Parameters.AddWithValue("@p_PODetails", obj.PODetails);
        //    var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMax != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}


        public bool SaveGRNPO(SaveGRNEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("GRN_spSaveGRNPO");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_InvoiceNo", obj.InvoiceNo);
            cmd.Parameters.AddWithValue("@p_InvoiceDate", obj.InvoiceDate);
            cmd.Parameters.AddWithValue("@p_Remarks", obj.Remarks);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
            cmd.Parameters.AddWithValue("@p_PODetails", obj.PODetails);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        
                                    
        public bool Update(GrnPOUpdate obj)
        {
            {
                bool res = false;
                SqlCommand cmd = new SqlCommand("BOM_spGRNStatusUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_GrnNo", obj.GrnNo);
                cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
                cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
                cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
                cmd.Parameters.AddWithValue("@p_PODetails", obj.PODetails);
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
        public IEnumerable<GrnEntity> select(int? GrnNo)
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchGRNDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_GrnNo", GrnNo);
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnEntity>(cmd);
            return locMas;
        }

        public IEnumerable<GrnEntity> GetAllGrn()
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchGRN");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GrnEntity>(cmd);
            return locMas;
        }

        public IEnumerable<getpo> GetAllRM()
        {
            SqlCommand cmd = new SqlCommand("Sp_GetGrnRM");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<getpo>(cmd);
            return locMas;
        }

        public IEnumerable<getpo> GetAllPrd()
        {
            SqlCommand cmd = new SqlCommand("Sp_GetGrnPrd");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<getpo>(cmd);
            return locMas;
        }


        
    }
}
