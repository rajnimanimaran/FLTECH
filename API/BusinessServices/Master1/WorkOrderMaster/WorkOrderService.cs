using BusinessEntities;
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

namespace BusinessServices.WorkOrder
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public WorkOrderEntity Create(WorkOrderEntity obj)
        {
            var WorkOrderEntity = new WorkOrderEntity();
            DataSet ds = new DataSet();
            bool res = false;
            SqlCommand cmd = new SqlCommand("WO_spSaveWorkOrder ");
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@p_GrnNo", obj.GrnNo);
            cmd.Parameters.AddWithValue("@p_WorkOrderID", obj.WorkOrderID);
            cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
            cmd.Parameters.AddWithValue("@p_ContID", obj.ContID);
            cmd.Parameters.AddWithValue("@p_AssignedDate", obj.AssignedDate);
            cmd.Parameters.AddWithValue("@p_Status", obj.Status);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_ExpectedDeliveryDate", obj.ExpectedDeliveryDate);
            cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
            ds = _unitOfWork.DbLayer.fillDataSet(cmd);
            WorkOrderEntity.getWOSODetails = ds.Tables[0].ConvertDataTableToEntityList<getWOSODetails>();
            //var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            //if (locMax != Int32.MaxValue)
            //{
            //    res = true;
            //}
            return WorkOrderEntity;
        }
        //public IEnumerable<WorkOrderGetEntity> GetAllsodm()
        //{
        //    SqlCommand cmd = new SqlCommand("sp_SelectWOMD");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<WorkOrderGetEntity>(cmd);
        //    return locMas;
        //}

        //public IEnumerable<getdropsalesorderID> Getsalesorderdrop()
        //{
        //    SqlCommand cmd = new SqlCommand("getSalesOrderIdWork");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<getdropsalesorderID>(cmd);
        //    return locMas;
        //}


        public IEnumerable<getWOSODetails> GetWorkOrdersoDetails(int WorkOrderID)
        {
            SqlCommand cmd = new SqlCommand("WO_spGetWODetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_WorkOrderID", WorkOrderID);
            var locMas = _unitOfWork.DbLayer.GetEntityList<getWOSODetails>(cmd);
            return locMas;
        }
        

  public IEnumerable<getWOSDetails> WOSdetails(int WODetlID)
        {
            SqlCommand cmd = new SqlCommand("sp_getWO_tserialdetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WODetlID", WODetlID);
            var locMas = _unitOfWork.DbLayer.GetEntityList<getWOSDetails>(cmd);
            return locMas;
        }
        public IEnumerable<getQCDetails> QCdetails(int PrdID)
        {
            SqlCommand cmd = new SqlCommand("sp_QCdetails");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_PrdID", PrdID);
            var locMas = _unitOfWork.DbLayer.GetEntityList<getQCDetails>(cmd);
            return locMas;
        }

        public IEnumerable<GetOS> GetOSList(int OrderStatusID)
        {
            SqlCommand cmd = new SqlCommand("Sp_GetOS");
            cmd.Parameters.AddWithValue("@p_OrderStatusID", OrderStatusID);
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<GetOS>(cmd);
            return locMas;
        }




        //public GetWorkOrderDetails GetWorkOrderDetails(int WorkOrderID)
        //{
        //    var getWorkOrderDetails = new GetWorkOrderDetails();
        //    DataSet ds = new DataSet();
        //    using (DbLayer dbLayer = new DbLayer())
        //    {
        //        SqlCommand SqlCmd = new SqlCommand("sp_GetWorkOrderDetails");
        //        SqlCmd.Parameters.AddWithValue("@WorkOrderID", WorkOrderID);
        //        SqlCmd.CommandType = CommandType.StoredProcedure;
        //        ds = _unitOfWork.DbLayer.fillDataSet(SqlCmd);
        //        getWorkOrderDetails.WorkOrder = ds.Tables[0].ConvertDataTableToEntityList<WorkOrders>().FirstOrDefault();
        //        getWorkOrderDetails.WorkOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<WorkOrderDetails>();
        //    }
        //    return getWorkOrderDetails;
        //}

        //public bool Create(WorkOrderEntity obj)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("sp_InsertWOMD");
        //    //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    ////cmd.Parameters.AddWithValue("@p_PurchaseID", obj.PurchaseID);
        //    cmd.Parameters.AddWithValue("@p_WorkOrderNumber", obj.WorkOrderNumber);
        //    cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
        //    cmd.Parameters.AddWithValue("@p_subContID", obj.subContID);
        //    cmd.Parameters.AddWithValue("@p_AssignedDate", obj.AssignedDate);
        //    cmd.Parameters.AddWithValue("@p_Status", obj.subContID);
        //    cmd.Parameters.AddWithValue("@p_ExpectedDeliveryDate", obj.AssignedDate);
        //    cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
        //    cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
        //    cmd.Parameters.AddWithValue("@p_WorkOrder_tDetails", obj.WorkOrder_tDetails);
        //    var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMax != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}

        //public bool Update(int WorkOrderID, WorkOrderEntity obj)
        //{
        //    bool res = false;
        //    SqlCommand cmd = new SqlCommand("sp_UpdateWOMD");
        //    //SqlCommand cmd = new SqlCommand("PO_spSavePurchaseOrder");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_WorkOrderID", obj.WorkOrderID);
        //    cmd.Parameters.AddWithValue("@p_WorkOrderNumber", obj.WorkOrderNumber);
        //    cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
        //    cmd.Parameters.AddWithValue("@p_subContID", obj.subContID);
        //    cmd.Parameters.AddWithValue("@p_AssignedDate", obj.AssignedDate);
        //    cmd.Parameters.AddWithValue("@p_Status", obj.Status);
        //    cmd.Parameters.AddWithValue("@p_ExpectedDeliveryDate", obj.ExpectedDeliveryDate);
        //    cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
        //    cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
        //    cmd.Parameters.AddWithValue("@p_WorkOrder_tDetails", obj.WorkOrder_tDetails);
        //    var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        //    if (locMas != Int32.MaxValue)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}


        ////public bool Delete(int ID, int WorkOrderID)
        ////{
        ////    bool res = false;
        ////    SqlCommand cmd = new SqlCommand("sp_PODDELETE");
        ////    cmd.CommandType = CommandType.StoredProcedure;
        ////    cmd.Parameters.AddWithValue("@p_ID", ID);
        ////    cmd.Parameters.AddWithValue("@p_WorkOrderID", WorkOrderID);
        ////    var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
        ////    if (locMas != Int32.MaxValue)
        ////    {
        ////        res = true;
        ////    }
        ////    return res;
        ////}


    }
}
