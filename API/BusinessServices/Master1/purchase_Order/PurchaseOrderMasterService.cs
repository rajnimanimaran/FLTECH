using BusinessEntities;
using BusinessEntities.purchase_Order;
using DataModel.UnitOfWork;
using DataModel.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.purchase_Order
{
public class PurchaseOrderMasterService: IPurchaseOrderMasterService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GetPurchaseDetails GetAllPurchaseDetails(int PurchaseID, int MaterialType)
        {
            var GetPurchaseDetails = new GetPurchaseDetails();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("PO_spGetPurchaseOrder");
            cmd.Parameters.AddWithValue("@p_PurchaseID", PurchaseID);
            cmd.Parameters.AddWithValue("@p_materialType", MaterialType);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = _unitOfWork.DbLayer.fillDataSet(cmd);
            GetPurchaseDetails.PurchaseOrderMaster = ds.Tables[0].ConvertDataTableToEntityList<PurchaseGetEntity>();
            if (ds.Tables.Count > 1)
            {
                GetPurchaseDetails.PurchaseOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<PurchaseOrderDetails>();
            }
            //var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(SqlCmd);

            return GetPurchaseDetails; ;
        }

        public GetPurchaseDetails GetPurchaseListByID(int PurchaseID, int ActionBy)
        {
            var GetPurchaseDetails = new GetPurchaseDetails();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("PUR_spGetPurchaseListByID");
            cmd.Parameters.AddWithValue("@p_PurchaseID", PurchaseID);
            cmd.Parameters.AddWithValue("@p_ActionBy", ActionBy);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = _unitOfWork.DbLayer.fillDataSet(cmd);
            GetPurchaseDetails.PurchaseOrderMaster = ds.Tables[0].ConvertDataTableToEntityList<PurchaseGetEntity>();
            if (ds.Tables.Count > 1)
            {
                GetPurchaseDetails.PurchaseOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<PurchaseOrderDetails>();
            }
            //var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(SqlCmd);

            return GetPurchaseDetails; ;
        }


        //public IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName()
        //{
        //    SqlCommand cmd = new SqlCommand("sp_getSupplierName");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<BusinessEntities.getSuppliername>(cmd);
        //    return locMas;
        //}


        public IEnumerable<PurchaseOrderMasterEntity> GetActivePurchaseList()
        {
            SqlCommand cmd = new SqlCommand("PO_spGetActivePurchaseList");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<PurchaseOrderMasterEntity>(cmd);
            return locMas;
        }
        //public IEnumerable<GetReportPurchase> Getreportpurchase()
        //{
        //    SqlCommand cmd = new SqlCommand("getReportPurchaseId");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<GetReportPurchase>(cmd);
        //    return locMas;
        //}
        //public IEnumerable<SalesGetReportID> SalesGetReportID()
        //{
        //    SqlCommand cmd = new SqlCommand("getReportOrderId");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<SalesGetReportID>(cmd);
        //    return locMas;
        //}
        //public GetPurchaseOrderDetails GetPurchaseOrderDetails(GetPurchaseOrderByID obj)
        //{
        //    GetPurchaseOrderDetails getPurchaseOrderDetails = new GetPurchaseOrderDetails();
        //    DataSet ds = new DataSet();
        //    using (DbLayer dbLayer = new DbLayer())
        //    {
        //        SqlCommand SqlCmd = new SqlCommand("BOM_spFetchBOMDetails");
        //        SqlCmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
        //        SqlCmd.Parameters.AddWithValue("@p_materialType", obj.materialType);
        //        SqlCmd.CommandType = CommandType.StoredProcedure;
        //        ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
        //        getPurchaseOrderDetails.PurchaseOrder = ds.Tables[0].ConvertDataTableToEntityList<PurchaseOrders>().FirstOrDefault();
        //        getPurchaseOrderDetails.PurchaseOrderDetailsEntity = ds.Tables[1].ConvertDataTableToEntityList<PurchaseOrderDetailsEntity>();
        //    }
        //    return getPurchaseOrderDetails;
        //}
        public bool Create(PurchaseOrderMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            ////cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_Code", obj.Code);
            cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_PurchaseDate", obj.PurchaseDate);
            cmd.Parameters.AddWithValue("@p_ExpectDeliveryDate", obj.ExpectDeliveryDate);
            cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Update(int PurchaseID, PurchaseOrderMasterEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_Code", obj.Code);
            cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_PurchaseDate", obj.PurchaseDate);
            cmd.Parameters.AddWithValue("@p_ExpectDeliveryDate", obj.ExpectDeliveryDate);
            cmd.Parameters.AddWithValue("@p_MaterialType", obj.MaterialType);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Delete(int PurchaseID, int ActionBy)
        {
            bool res = false;
            SqlCommand SqlCmd = new SqlCommand("BOM_spRemovePODetails");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@p_PurchaseID", PurchaseID);
            SqlCmd.Parameters.AddWithValue("@p_ActionBy", ActionBy);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public IEnumerable<PurchaseOrderMDEntity> get(GetPurchaseOrderByID obj)
        {
            SqlCommand cmd = new SqlCommand("BOM_spFetchBOMDetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_MaterialType", obj.materialType);
            var locMas = _unitOfWork.DbLayer.GetEntityList<PurchaseOrderMDEntity>(cmd);
            return locMas;
        }

    }
}

