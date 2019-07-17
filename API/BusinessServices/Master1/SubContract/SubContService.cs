using BusinessEntities.Master1.SubContract;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Master1.SubContract
{
  public  class SubContService : ISubCont
    {
             private readonly IUnitOfWork _unitOfWork;

            public SubContService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public IEnumerable<SubContEntity> GetAllsub()
            {
                SqlCommand cmd = new SqlCommand("SUB_spGetWorkSubMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                var locMas = _unitOfWork.DbLayer.GetEntityList<SubContEntity>(cmd);
                return locMas;
            }
            public bool Createsub(SubContEntity obj)
            {
                bool res = false;
                SqlCommand cmd = new SqlCommand("SUB_spSaveSubMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_Name", obj.Name);
                cmd.Parameters.AddWithValue("@p_Address", obj.Address);
                cmd.Parameters.AddWithValue("@p_GstNo", obj.GstNo);
                cmd.Parameters.AddWithValue("@p_contactNumber", obj.contactNumber);
                cmd.Parameters.AddWithValue("@p_supplierID", obj.supplierID);
                cmd.Parameters.AddWithValue("@p_emailID", obj.emailID);
                cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);

                var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
                if (locMax != Int32.MaxValue)
                {
                    res = true;
                }
                return res;
            }
            public bool Updatesub(int subContID, SubContEntity obj)
            {
                bool res = false;
                SqlCommand cmd = new SqlCommand("SUB_spSaveSubMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_subContID", obj.subContID);
                cmd.Parameters.AddWithValue("@p_Name", obj.Name);
                cmd.Parameters.AddWithValue("@p_Address", obj.Address);
                cmd.Parameters.AddWithValue("@p_GstNo", obj.GstNo);
                cmd.Parameters.AddWithValue("@p_contactNumber", obj.contactNumber);
                cmd.Parameters.AddWithValue("@p_supplierID", obj.supplierID);
                cmd.Parameters.AddWithValue("@p_emailID", obj.emailID);
                cmd.Parameters.AddWithValue("@p_ActionBy", obj.ActionBy);
                cmd.Parameters.AddWithValue("@p_IsActive", obj.IsActive);
                var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
                if (locMax != Int32.MaxValue)
                {
                    res = true;
                }
                return res;

            }
            public bool Deletesub(int subContID)
            {
                bool res = false;
                SqlCommand cmd = new SqlCommand("SUB_spRemoveSubMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_subContID", subContID);
                var locMax = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
                if (locMax != Int32.MaxValue)
                {
                    res = true;
                }
                return res;

            }

        }
    }

 
