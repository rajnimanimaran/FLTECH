using BusinessEntities;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.QualityAudit
{
    public class QualityAuditService : IQualityAuditService
    {


        private readonly IUnitOfWork _unitOfWork;

        public QualityAuditService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QualityAuditEntity> GetAllQCAuditDetails()
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchQCAuditDetail");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<QualityAuditEntity>(cmd);
            return locMas;
        }
        public bool CreateQCAudit(QualityAuditEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("QC_spSaveQCAudit");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_QCAuditID", obj.QCAuditID);
            cmd.Parameters.AddWithValue("@p_QCID", obj.QCID);
            cmd.Parameters.AddWithValue("@p_WOPRDSerialID", obj.WOPRDSerialID);
            cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_WODetlID", obj.WODetlID);
            cmd.Parameters.AddWithValue("@p_ApprovedQuantity", obj.ApprovedQuantity);
            cmd.Parameters.AddWithValue("@p_RejectedQuantity", obj.RejectedQuantity);
            cmd.Parameters.AddWithValue("@p_ReworkQuantity", obj.ReworkQuantity);
            cmd.Parameters.AddWithValue("@p_prdID", obj.prdID);
            cmd.Parameters.AddWithValue("@p_QCAuditDetails", obj.QCAuditDetails);
            //cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            //cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
            //cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
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


        public bool UpdateQCAuditDetails(QualityAuditEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spRemoveQCAuditDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_QCAuditID", obj.QCAuditID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            //cmd.Parameters.AddWithValue("@p_RMCode", obj.RMCode);
            //cmd.Parameters.AddWithValue("@p_RMName", obj.RMName);
            //cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            //cmd.Parameters.AddWithValue("@p_splitable", obj.splitable);
            //cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            //cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            //cmd.Parameters.AddWithValue("@p_HSNCode", obj.HSNCode);
            //cmd.Parameters.AddWithValue("@p_sell_price", obj.sell_price);
            //cmd.Parameters.AddWithValue("@p_cost_price", obj.cost_price);
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
        
        //private readonly IUnitOfWork _unitOfWork;
        //public QualityAuditService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //public bool Create(QualityAuditEntity obj)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("QC_spSaveQCAudit");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_QCID", obj.QCID);
        //    cmd.Parameters.AddWithValue("@p_WOPRDSerialID", obj.WOPRDSerialID);
        //    cmd.Parameters.AddWithValue("@p_StatusID", obj.StatusID);
        //    cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
        //    cmd.Parameters.AddWithValue("@p_QCAuditDetails", obj.QCAuditDetails);
        //    cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

        //    var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMax != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}
    }
}
