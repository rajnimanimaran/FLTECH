
using BusinessEntities.BOM_Master;
using DataModel.DBLayer;
using DataModel.UnitOfWork;
using DataModel.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.BOM_Master
{
   public class BOMmasterServices: IBOMmasterServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public BOMmasterServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GetBOM GetAllBOM_Master(int BOMID )
        {

            var GetBOM = new GetBOM();
            DataSet ds = new DataSet();
            using (DbLayer dbLayer = new DbLayer())
            {
                SqlCommand SqlCmd = new SqlCommand("BOM_spFetchBOMDetails");
                SqlCmd.Parameters.AddWithValue("@p_BOMID", BOMID);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
                GetBOM.BOM_master = ds.Tables[0].ConvertDataTableToEntityList<BOM_masterEntity>();
                if (ds.Tables.Count > 1)
                {
                    GetBOM.BOM_details = ds.Tables[1].ConvertDataTableToEntityList<BOM_detailsEntity>();
                }
                //var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(SqlCmd);
            }
            return GetBOM;
        }
        public bool CreateBOM_master(BOM_masterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spSaveBOMDetails");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_BOMCode", obj.BOMCode);
            cmd.Parameters.AddWithValue("@p_BOMName", obj.BOMName);
            cmd.Parameters.AddWithValue("@p_prdID", obj.prdID);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_BOMDetails", obj.BOMDetails);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdateBOM_master(int BOMID, BOM_masterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spSaveBOMDetails");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_BOMID", obj.BOMID);
            cmd.Parameters.AddWithValue("@p_BOMCode", obj.BOMCode);
            cmd.Parameters.AddWithValue("@p_BOMName", obj.BOMName);
            cmd.Parameters.AddWithValue("@p_prdID", obj.prdID);
            //cmd.Parameters.AddWithValue("@p_RMID", obj.RMID);
            //cmd.Parameters.AddWithValue("@p_quantity", obj.quantity);
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
        public bool DeleteBOM_master(int BOMID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("BOM_spRemoveBOM_Master");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_BOMID", BOMID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
    }
}
