using BusinessEntities;
using DataModel.UnitOfWork;
using DataModel.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class SalesOrderService: ISalesOrderService
    {

        private readonly IUnitOfWork _unitOfWork;

        public SalesOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public GetSalesDetails GetAllsales( int OrderID)
        {
            var GetSalesDetails = new GetSalesDetails();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SO_spGetsalesOrder");
            cmd.Parameters.AddWithValue("@p_OrderID", OrderID);
            cmd.CommandType = CommandType.StoredProcedure;
            ds = _unitOfWork.DbLayer.fillDataSet(cmd);
            GetSalesDetails.SalesOrderMaster = ds.Tables[0].ConvertDataTableToEntityList<SalesOrderMaster>();
            if (ds.Tables.Count > 1)
            {
                GetSalesDetails.SalesOrderDetails = ds.Tables[1].ConvertDataTableToEntityList<SalesOrderDetails>();
            }
            //var locMas = _unitOfWork.DbLayer.GetEntityList<GetAllQCMDetails>(SqlCmd);
        
            return GetSalesDetails;;
        }

        public bool Create(SalesOrderMaster obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SO_spSaveSalesOrder");
            cmd.CommandType = CommandType.StoredProcedure;
           // cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ACKDate", obj.ACKDate);
            cmd.Parameters.AddWithValue("@p_AssignedTo", obj.AssignedTo);
            cmd.Parameters.AddWithValue("@p_ActionBy ", obj.ActionBy);
            //cmd.Parameters.AddWithValue("@p_CreatedOn ", obj.CreatedOn);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_CustomerID", obj.CustId);
            cmd.Parameters.AddWithValue("@p_OrderTakenBy", obj.OrderTakenBy);
            cmd.Parameters.AddWithValue("@p_OrderStatus", obj.OrderStatus);
            cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Update(int OrderID, SalesOrderMaster obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SO_spSaveSalesOrder");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderID", obj.OrderID);
           // cmd.Parameters.AddWithValue("@p_CODE", obj.CODE);
            cmd.Parameters.AddWithValue("@p_OrderDate", obj.OrderDate);
            cmd.Parameters.AddWithValue("@p_ACKDate", obj.ACKDate);
            cmd.Parameters.AddWithValue("@p_AssignedTo", obj.AssignedTo);
            cmd.Parameters.AddWithValue("@p_ActionBy ", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            cmd.Parameters.AddWithValue("@p_CustomerID", obj.CustId);
            cmd.Parameters.AddWithValue("@p_OrderTakenBy", obj.OrderTakenBy);
            cmd.Parameters.AddWithValue("@p_OrderStatus", obj.OrderStatus);
            cmd.Parameters.AddWithValue("@p_OrderDetails", obj.OrderDetails);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public bool Delete(int OrderID, int ActionBy)
        {
            bool res = false;
            SqlCommand SqlCmd = new SqlCommand("SO_spRemoveSalesDetails");
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@p_OrderID", OrderID);
            SqlCmd.Parameters.AddWithValue("@p_ActionBy", ActionBy);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(SqlCmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        //public IEnumerable<SalesOrderMasterEntity> get(int OrderID,int ActionBy)
        //{
        //    SqlCommand cmd = new SqlCommand("SO_spFetchSalesOrder");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@p_OrderID", OrderID);
        //    cmd.Parameters.AddWithValue("@p_ActionBy",ActionBy);
        //    var locMas = _unitOfWork.DbLayer.GetEntityList<SalesOrderMasterEntity>(cmd);
        //    return locMas;
        //}


        public bool InsertSalesOrderPlan(SalesOrderPlanEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SO_spSavePlanning");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderSetID", obj.OrderSetID);
            cmd.Parameters.AddWithValue("@p_SOID", obj.SOID);
            cmd.Parameters.AddWithValue("@p_SORefID", obj.SORefID);
            cmd.Parameters.AddWithValue("@p_Quantity", obj.Quantity);
            cmd.Parameters.AddWithValue("@p_Deliverydate", obj.Deliverydate);
            cmd.Parameters.AddWithValue("@p_OrdAckSdate ", obj.OrdAckSdate);
            cmd.Parameters.AddWithValue("@p_OrdAckEdate ", obj.OrdAckEdate);
            cmd.Parameters.AddWithValue("@p_MatRcvInFacSdate", obj.MatRcvInFacSdate);
            cmd.Parameters.AddWithValue("@p_MatRcvInFacEdate", obj.MatRcvInFacEdate);
            cmd.Parameters.AddWithValue("@p_MatInFlowSdate", obj.MatInFlowSdate);
            cmd.Parameters.AddWithValue("@p_MatInFlowEdate", obj.MatInFlowEdate);
            cmd.Parameters.AddWithValue("@p_AssembleSdate", obj.AssembleSdate);
            cmd.Parameters.AddWithValue("@p_AssembleEdate", obj.AssembleEdate);
            cmd.Parameters.AddWithValue("@p_HydrolicSdate", obj.HydrolicSdate);
            cmd.Parameters.AddWithValue("@p_HydrolicEdate", obj.HydrolicEdate);
            cmd.Parameters.AddWithValue("@p_PerfTestSdate", obj.PerfTestSdate);
            cmd.Parameters.AddWithValue("@p_PerfTestEdate", obj.PerfTestEdate);
            cmd.Parameters.AddWithValue("@p_PaintingSdate", obj.PaintingSdate);
            cmd.Parameters.AddWithValue("@p_PaintingEdate", obj.PaintingEdate);
            cmd.Parameters.AddWithValue("@p_FinalInspSdate", obj.FinalInspSdate);
            cmd.Parameters.AddWithValue("@p_FinalInspEdate", obj.FinalInspEdate);
            cmd.Parameters.AddWithValue("@p_PackingSdate", obj.PackingSdate);
            cmd.Parameters.AddWithValue("@p_PackingEdate", obj.PackingEdate);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }



        public bool UpdateSalesOrderPlan(int OrderSetID, SalesOrderPlanEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("SO_spSavePlanning");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_OrderSetID", obj.OrderSetID);
            cmd.Parameters.AddWithValue("@p_SOID", obj.SOID);
            cmd.Parameters.AddWithValue("@p_SORefID", obj.SORefID);
            cmd.Parameters.AddWithValue("@p_Quantity", obj.Quantity);
            cmd.Parameters.AddWithValue("@p_OrdAckSdate ", obj.OrdAckSdate);
            cmd.Parameters.AddWithValue("@p_OrdAckEdate ", obj.OrdAckEdate);
            cmd.Parameters.AddWithValue("@p_MatRcvInFacSdate", obj.MatRcvInFacSdate);
            cmd.Parameters.AddWithValue("@p_MatRcvInFacEdate", obj.MatRcvInFacEdate);
            cmd.Parameters.AddWithValue("@p_MatInFlowSdate", obj.MatInFlowSdate);
            cmd.Parameters.AddWithValue("@p_MatInFlowEdate", obj.MatInFlowEdate);
            cmd.Parameters.AddWithValue("@p_AssembleSdate", obj.AssembleSdate);
            cmd.Parameters.AddWithValue("@p_AssembleEdate", obj.AssembleEdate);
            cmd.Parameters.AddWithValue("@p_HydrolicSdate", obj.HydrolicSdate);
            cmd.Parameters.AddWithValue("@p_HydrolicEdate", obj.HydrolicEdate);
            cmd.Parameters.AddWithValue("@p_PerfTestSdate", obj.PerfTestSdate);
            cmd.Parameters.AddWithValue("@p_PerfTestEdate", obj.PerfTestEdate);
            cmd.Parameters.AddWithValue("@p_PaintingSdate", obj.PaintingSdate);
            cmd.Parameters.AddWithValue("@p_PaintingEdate", obj.PaintingEdate);
            cmd.Parameters.AddWithValue("@p_FinalInspSdate", obj.FinalInspSdate);
            cmd.Parameters.AddWithValue("@p_FinalInspEdate", obj.FinalInspEdate);
            cmd.Parameters.AddWithValue("@p_PackingSdate", obj.PackingSdate);
            cmd.Parameters.AddWithValue("@p_PackingEdate", obj.PackingEdate);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

        public IEnumerable<GetSalesOrderCodeEntity> GetSalesCode()
        {
            SqlCommand cmd = new SqlCommand("SO_spGetSalesOrderCode");
            cmd.CommandType = CommandType.StoredProcedure;
            var SalesCode = _unitOfWork.DbLayer.GetEntityList<GetSalesOrderCodeEntity>(cmd);

            return SalesCode;

        }


        public IEnumerable<SalesOrderPlanEntity> GetSalesOrderPlan(int SO_RefID)
        {
            SqlCommand cmd = new SqlCommand("SO_spGetPlanningDetails");
            cmd.Parameters.AddWithValue("@p_SORefID", SO_RefID);
            cmd.CommandType = CommandType.StoredProcedure;
            var SalesCode = _unitOfWork.DbLayer.GetEntityList<SalesOrderPlanEntity>(cmd);

            return SalesCode;

        }
    }
}


  
