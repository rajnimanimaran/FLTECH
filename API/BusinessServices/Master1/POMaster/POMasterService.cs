using BusinessEntities;
using BusinessEntities.Master1.SalesOrder;
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

namespace BusinessServices.Master1.POMaster
{
    public class POMasterService : IPOMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public POMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName()
        {
            SqlCommand cmd = new SqlCommand("sp_getSupplierName");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<BusinessEntities.getSuppliername>(cmd);
            return locMas;
        }


        public IEnumerable<PurchaseGetEntity> GetAllsodm()
        {
            SqlCommand cmd = new SqlCommand("PO_spGetPurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<PurchaseGetEntity>(cmd);
            return locMas;
        }
        public IEnumerable<GetReportPurchase> Getreportpurchase()
        {
            SqlCommand cmd = new SqlCommand("getReportPurchaseId");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GetReportPurchase>(cmd);
            return locMas;
        }
        public IEnumerable<SalesGetReportID> SalesGetReportID()
        {
            SqlCommand cmd = new SqlCommand("getReportOrderId");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<SalesGetReportID>(cmd);
            return locMas;
        }
        public GetPurchaseOrderDetails GetPurchaseOrderDetails(int PurchaseID)
        {
            var getPurchaseOrderDetails = new GetPurchaseOrderDetails();
            DataSet ds = new DataSet();
            using (DbLayer dbLayer = new DbLayer())
            {
                SqlCommand SqlCmd = new SqlCommand("sp_GetPODetails");
                SqlCmd.Parameters.AddWithValue("@PurchaseID", PurchaseID);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
                getPurchaseOrderDetails.PurchaseOrder = ds.Tables[0].ConvertDataTableToEntityList<PurchaseOrders>().FirstOrDefault();
                getPurchaseOrderDetails.PurchaseOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<PurchaseOrderDetails>();
            }
            return getPurchaseOrderDetails;
        }
        public bool Create(PurchaseOrderCommonEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_PODInsert");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            ////cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_Code", obj.Code);
            cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_PurchaseDate", obj.PurchaseDate);
            cmd.Parameters.AddWithValue("@p_ExpectDeliveryDate", obj.ExpectDeliveryDate);
            cmd.Parameters.AddWithValue("@p_InvoiceNo", obj.InvoiceNo);
            cmd.Parameters.AddWithValue("@p_InvoiceDate", obj.InvoiceDate);
            cmd.Parameters.AddWithValue("@p_TransportMode", obj.TransportMode);
            cmd.Parameters.AddWithValue("@p_VechicleNumber", obj.VechicleNumber);
            cmd.Parameters.AddWithValue("@p_DateOfSupply", obj.DateOfSupply);
            cmd.Parameters.AddWithValue("@p_PlaceOfSupply", obj.PlaceOfSupply);
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

        public bool Update(int PurchaseID, PurchaseOrderCommonEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_PODUPDATE");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
            cmd.Parameters.AddWithValue("@p_Code", obj.Code);
            cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_PurchaseDate", obj.PurchaseDate);
            cmd.Parameters.AddWithValue("@p_ExpectDeliveryDate", obj.ExpectDeliveryDate);
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

        //public bool Delete(int ID, int PurchaseID)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("sp_PODDELETE");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_ID", ID);
        //    cmd.Parameters.AddWithValue("@p_PurchaseID", PurchaseID);
        //    var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMas != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}
        public bool Delete(int PurchaseID)
        {
            bool res = false;
            SqlCommand SqlCmd = new SqlCommand("sp_PODDELETE");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

    
    public IEnumerable<SalesGetEntity> GetAllpodm()
         {
            SqlCommand cmd = new SqlCommand("SO_spGetSalesOrderMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<SalesGetEntity>(cmd);
            return locMas;
        }

        public GetSalesOrderDetails GetSalesOrderDetails(int OrderID)
        {
            var getSalesOrderDetails = new GetSalesOrderDetails();
            DataSet ds = new DataSet();
            using (DbLayer dbLayer = new DbLayer())
            {
                SqlCommand SqlCmd = new SqlCommand("sp_GetSODetails");
                SqlCmd.Parameters.AddWithValue("@OrderID", OrderID);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
                getSalesOrderDetails.SalesOrder = ds.Tables[0].ConvertDataTableToEntityList<SalesOrders>().FirstOrDefault();
                //getSalesOrderDetails.SalesOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<SalesOrderDetails>();
            }
            return getSalesOrderDetails;
        }

        public bool Create(SalesOrderCommonEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SP_SODInsert");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
            cmd.Parameters.AddWithValue("@p_OrderNumber", obj.OrderNumber);
            cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
             cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ACKDate", obj.ACKDate);
            cmd.Parameters.AddWithValue("@p_AssignedTo", obj.AssignedTo);
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
        public bool Update(int OrderID, SalesOrderCommonEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SP_SODUpdate");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
            cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
            cmd.Parameters.AddWithValue("@p_OrderNumber", obj.OrderNumber);
            cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ACKDate", obj.ACKDate);
            cmd.Parameters.AddWithValue("@p_AssignedTo", obj.AssignedTo);
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

        public bool DeleteS(int OrderID)
        {
            bool res = false;
            SqlCommand SqlCmd = new SqlCommand("sp_SODDELETE");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@OrderID", OrderID);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }


        public DataSet ReturnPurchaseList(int PurchaseID,PurchaseGetReportEntity obj)
        {
            bool res = false;
            PurchaseGetReportEntity activeList = new PurchaseGetReportEntity();
            //using (DblLayer dbLayer = new DblLayer())
            //{
            SqlCommand SqlCmd = new SqlCommand("sp_PurchaseMasterReport");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@PurchaseID", PurchaseID);
            var ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
            activeList.PurchaseID = Convert.ToInt32(ds.Tables[0].Rows[0]["PurchaseID"].ToString());
            activeList.code = (ds.Tables[0].Rows[0]["Code"].ToString());
            activeList.SID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"].ToString());
            activeList.SName = (ds.Tables[0].Rows[0]["SName"].ToString());
            activeList.SPhNo = (ds.Tables[0].Rows[0]["SPhNo"].ToString());
            activeList.SGSTNo = Convert.ToInt32(ds.Tables[0].Rows[0]["SGSTNo"].ToString());
            activeList.SState = (ds.Tables[0].Rows[0]["SState"].ToString());
            activeList.SStateCode = Convert.ToInt32(ds.Tables[0].Rows[0]["SStateCode"].ToString());
            activeList.PurchaseDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PurchaseDate"].ToString());
            activeList.ExpectDeliveryDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpectDeliveryDate"].ToString());
            activeList.PurchaseReport = ds.Tables[1];

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return ds;
            //}

        }
        public DataSet ReturnSalesList(int OrderID, SalesGetReportEntity obj)
        {
            bool res = false;
            SalesGetReportEntity activeList = new SalesGetReportEntity();
            //using (DblLayer dbLayer = new DblLayer())
            //{
            SqlCommand SqlCmd = new SqlCommand("sp_SalesMasterReport");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@OrderID", OrderID);
            var ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
            activeList.OrderID = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderID"].ToString());
            activeList.OrderNumber = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderNumber"].ToString());
            activeList.code = (ds.Tables[0].Rows[0]["Code"].ToString());
            activeList.SID = Convert.ToInt32(ds.Tables[0].Rows[0]["SupplierID"].ToString());
            activeList.SName = (ds.Tables[0].Rows[0]["SName"].ToString());
            activeList.AssignedTo = (ds.Tables[0].Rows[0]["AssignedTo"].ToString());
            activeList.OrderDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["OrderDate"].ToString());
            activeList.ACKDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ACKDate"].ToString());
            activeList.SalesReport = ds.Tables[1];

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return ds;
            //}

        }

    }





    }


