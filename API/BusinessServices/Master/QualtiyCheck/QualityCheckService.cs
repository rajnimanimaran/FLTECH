using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Master1;
using DataModel.DBLayer;
using DataModel.UnitOfWork;
using DataModel.Utilities;

namespace BusinessServices.Master.QualtiyCheck
{


    public class QualityCheckService : IQualityCheckService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QualityCheckService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<GetAllQCMDetails> GetAllQCheck( )
        {
            SqlCommand cmd = new SqlCommand("QC_spFetchQCDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(cmd);
            return locMas;
        }


        public GetAllQCMDetails GetAllQCMDetails(int QCID)
        {
            var GetAllQCMDetails = new GetAllQCMDetails();
            DataSet ds = new DataSet();
            using (DbLayer dbLayer = new DbLayer())
            {
                SqlCommand SqlCmd = new SqlCommand("QC_spFetchQCDetails");
                SqlCmd.Parameters.AddWithValue("@p_QCID", QCID);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
                GetAllQCMDetails.QCCheckMaster = ds.Tables[0].ConvertDataTableToEntityList<QCCheckMaster>();
                if (ds.Tables.Count >1)
                {
                    GetAllQCMDetails.QCCheckDetails = ds.Tables[1].ConvertDataTableToEntityList<QCCheckDetails>();
                }
                //var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(SqlCmd);
            }
            return GetAllQCMDetails;
             
        }

        public bool Create(InsertQCEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("QC_spSaveQCDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_QCCode", obj.QCCode);
            cmd.Parameters.AddWithValue("@p_QCName", obj.QCName);
            cmd.Parameters.AddWithValue("@p_PrdID", obj.PrdID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_QCDetails", obj.QCDetails);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Update(int QCID, UpdateQCEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("QC_spSaveQCDetails");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_QCID", obj.QCID);
            cmd.Parameters.AddWithValue("@p_QCCode", obj.QCCode);
            cmd.Parameters.AddWithValue("@p_QCName", obj.QCName);
            cmd.Parameters.AddWithValue("@p_PrdID", obj.PrdID);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_QCDetails", obj.QCDetails);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Delete(int QCID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("QC_spDeleteQCDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_QCID", QCID);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
       
    }
}
