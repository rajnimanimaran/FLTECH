using BusinessEntities.Master1.ProductStock;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.ProductStock
{
    public class ProductStockService : IProductStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductStockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductStockEntity> GetAllPrdSk()
        {
            SqlCommand cmd = new SqlCommand("PRDST_spGetproductStock");
            cmd.CommandType = CommandType.StoredProcedure;
            var locMas = _unitOfWork.DbLayer.GetEntityList<ProductStockEntity>(cmd);
            return locMas;
        }
        public bool CreatePrdSk(ProductStockEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRDST_spSaveProductStock");
            cmd.CommandType = CommandType.StoredProcedure;
         
            cmd.Parameters.AddWithValue("@p_prdID", obj.prdID);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }
        public bool UpdatePrdSk(int StockID, ProductStockEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRDST_spSaveProductStock");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_StockID", obj.StockID);
            cmd.Parameters.AddWithValue("@p_prdID",obj. prdID);
            //cmd.Parameters.AddWithValue("@p_prdName", obj.prdName);
            cmd.Parameters.AddWithValue("@p_UOMID", obj.UOMID);
            cmd.Parameters.AddWithValue("@p_stock", obj.stock);
            cmd.Parameters.AddWithValue("@p_re_Orderlevel", obj.re_Orderlevel);
            cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
            cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
        public bool DeletePrdSk(int StockID)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("PRDST_spRemoveproductStock");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_StockID", StockID);
            var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMax != Int32.MaxValue)
            {
                res = true;
            }
            return res;

        }
    }
}
