using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ActionFilters;
using WebApi.ErrorHelper;
using System.Web.Script.Serialization;
using System.Text;

namespace WebApi.Controllers
{
    [RoutePrefix("EmployeeChart")]
    public class EmployeeChartController : ApiController
    {
        //private readonly IEmployeeServices _employeeServices;

        //public EmployeeChartController(IEmployeeServices employee)
        //{
        //    _employeeServices = employee;
        //}

        //EmpOrgChartService

        [HttpGet]
        [Route("View")]
        public HttpResponseMessage Get()
        {
            try
            {
                Service.EmpOrgChartService empService = new Service.EmpOrgChartService();

                DataTable dtEmployeeDetails = new DataTable();

                dtEmployeeDetails = empService.EmployeeDetailsRead_ForChart("");

                var result = DataTableToJSONWithStringBuilder(dtEmployeeDetails);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company Not Found", HttpStatusCode.NotFound);
            }
        }

        public string DataTableToJSONWithStringBuilder(DataTable dt)
        {
            var JSONString = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                JSONString.Append(@"{ ""cols"" : [ {""label"": ""Name"", ""pattern"": """", ""type"": ""string""}, {""label"": ""Manager"", ""pattern"": """", ""type"": ""string""},{""label"": ""ToolTip"", ""pattern"": """", ""type"": ""string""}], ""rows"" : [");


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JSONString.Append(@"{""c"":[{""v"" : """ + dt.Rows[i]["EmployeeId"].ToString() + @""",""f"" : """ + dt.Rows[i]["EmployeeName"].ToString() + @"""},");

                    string strManager = Convert.ToInt32(dt.Rows[i]["ManagerId"]) > 0 ? dt.Rows[i]["ManagerId"].ToString() : string.Empty;

                    JSONString.Append(@"{""v"" : """ + strManager + @"""},");
                    JSONString.Append(@"{""v"" : """ + dt.Rows[i]["DesignationName"].ToString() + @"""}]");

                    if (i == dt.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]}");
            }
            return JSONString.ToString();
        }

        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

    }
}