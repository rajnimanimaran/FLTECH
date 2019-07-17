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

namespace BusinessServices.Master1.SOMaster
{
   public class SOMasterService : ISOMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SOMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SalesGetEntity> GetAllpodm()
        {
            SqlCommand cmd = new SqlCommand("sp_selectt_PurchaseMaster");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<SalesGetEntity>(cmd);
            return locMas;
        }

        //public IEnumerable<BusinessEntities.getSuppliername> GetAllSupplierName()
        //{
        //    SqlCommand cmd = new SqlCommand("sp_getSupplierName");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<BusinessEntities.getSuppliername>(cmd);
        //    return locMas;
        //}



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
                getSalesOrderDetails.SalesOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<SalesOrderDetails>();
            }
            return getSalesOrderDetails;
        }

        public bool Create(SalesOrderCommonEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_SODInsert");
            //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
            cmd.Parameters.AddWithValue("@p_OrderNumber", obj.OrderNumber);
            cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
            // cmd.Parameters.AddWithValue("@p_CustomerID", obj.CustomerID);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ ACKDate", obj.ACKDate);
            // cmd.Parameters.AddWithValue("@p_OrderTakenBy", obj.OrderTakenBy);
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




        //public bool Update(int SalesID, PurchaseOrderCommonEntity obj)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("sp_SODUPDATE");
        //    //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_SalesID", obj.SalesID);
        //    cmd.Parameters.AddWithValue("@p_Code", obj.Code);
        //    cmd.Parameters.AddWithValue("@p_SupplierID", obj.SupplierID);
        //    cmd.Parameters.AddWithValue("@p_PurchaseDate", obj.PurchaseDate);
        //    cmd.Parameters.AddWithValue("@p_ExpectDeliveryDate", obj.ExpectDeliveryDate);
        //    cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
        //    cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
        //    cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
        //    var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMas != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}

        //public bool Delete(int ID, int PurchaseID)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("sp_SODDELETE");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_ID", ID);
        //    cmd.Parameters.AddWithValue("@p_SalesID", SalesID);
        //    var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMas != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}

    }
}
   