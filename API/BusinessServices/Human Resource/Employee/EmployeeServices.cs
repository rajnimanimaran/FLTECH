using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<EmployeeEntity> GetAllEmployee(int employeeId)
        {
            SqlCommand cmd = new SqlCommand("sp_ViewEmployee");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_employeeId", employeeId);
            var locMas = _unitOfWork.DbLayer.GetEntityList<EmployeeEntity>(cmd);

            return locMas;

        }

        public bool CreateEmployee(EmployeeEntity obj)
        {
            bool res = false;
            SqlCommand cmd = new SqlCommand("sp_SaveEmployee");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@p_employeeName", obj.employeeName);
            cmd.Parameters.AddWithValue("@p_departmentName", obj.departmentName);
            cmd.Parameters.AddWithValue("@p_designation", obj.designation);
            var locMas = _unitOfWork.DbLayer.ExecuteNonQuery(cmd);
            if (locMas != Int32.MaxValue)
            {
                res = true;
            }
            return res;
        }

    }
}
