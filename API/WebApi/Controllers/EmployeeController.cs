using BusinessEntities;
using BusinessServices;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employee)
        {
            _employeeServices = employee;
        }

        [HttpGet]
        [Route("AllEmployees/{employeeId}")]
        public HttpResponseMessage GetAllEmployee(int employeeId)
        {
            try
            {
                var employees = _employeeServices.GetAllEmployee(employeeId);
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.NotFound);
            }
        }
        //[HttpPost]
        //[Route("FileUpload")]
        //public HttpResponseMessage FileUpload()
        //{
        //    try
        //    {
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //        }
        //        HttpResponseMessage message;
        //        var Attachment = HttpContext.Current.Request.Files["fileAttach"];
        //        var FileUrl = Attachment.FileName;
        //        if (Attachment != null && FileUrl != null)
        //        {
        //            var pathf = HttpContext.Current.Server.MapPath("~/ProfilePictures/");
        //            var fileSavePath = Path.Combine(pathf, FileUrl);
        //            Directory.CreateDirectory(pathf);
        //            Attachment.SaveAs(fileSavePath);
        //        }

        //        return message = Request.CreateResponse(HttpStatusCode.OK, new { msgText = "Success!", result = FileUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Unit Master not found", HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpGet]
        //[Route("AllCompanyEmployees/{DesignationId}")]
        //public HttpResponseMessage GetAllCompanyEmployees(int DesignationId)
        //{
        //    try
        //    {
        //        var employees = _employeeServices.GetAllCompanyEmployees(DesignationId);
        //        return Request.CreateResponse(HttpStatusCode.OK, employees);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpGet]
        //[Route("GetEmployeeId/{EmployeeId}")]
        //public HttpResponseMessage GetById(int EmployeeId)
        //{
        //    if (EmployeeId > 0)
        //    {
        //        var employee = _employeeServices.GetEmployeeById(EmployeeId);
        //        if (employee != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, employee);
        //        throw new ApiDataException(1001, "No employee found for this id.", HttpStatusCode.NotFound);
        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}

        [HttpPost]
        [Route("Create")]
        public bool CreateEmployee([FromBody]EmployeeEntity employeeEntity)
        {
            try
            {
                return _employeeServices.CreateEmployee(employeeEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Created as temptemporary solution, needed change in future.
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("uploadProfilePic")]
        //public HttpResponseMessage UploadProfilePicture()
        //{
        //    try
        //    {
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //        }
        //        var profilePic = HttpContext.Current.Request.Files["profilePic"];
        //        var employeeId = Convert.ToInt32(HttpContext.Current.Request.Form["employeeId"]);
        //        string path = HttpContext.Current.Server.MapPath("~/EmployeeProfilePictures");
        //        bool folderExists = Directory.Exists(path);

        //        if (!folderExists)
        //            Directory.CreateDirectory(path);

        //        var fileName = DateTime.Now.Ticks + Path.GetExtension(profilePic.FileName);
        //        var fileSavePath = Path.Combine(path, fileName);
        //        profilePic.SaveAs(fileSavePath);
        //        var empData = new EmpProPicUploadInputDTO();

        //        if (File.Exists(fileSavePath))
        //        {
        //            var emp = _employeeServices.GetEmployeeById(employeeId);
        //            var fileToDelete = Path.Combine(path, emp.ProfilePhoto);
        //            File.Delete(fileToDelete);
        //            empData.EmployeeId = employeeId;
        //            empData.ProfilePhoto = fileName;
        //        }
        //        var result = _employeeServices.EmployeeProPicUpload(empData);
        //        return Request.CreateResponse(HttpStatusCode.OK, "success!");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpPost]
        //[Route("createDoc")]
        //public HttpResponseMessage PostDoc()
        //{
        //    try
        //    {
        //        if (!Request.Content.IsMimeMultipartContent())
        //        {
        //            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //        }

        //        var profilePic = HttpContext.Current.Request.Files["profilePic"];
        //        //var httpPostedFiles2 = HttpContext.Current.Request.Files["fileList2"];

        //        var data = HttpContext.Current.Request.Form["data"];

        //        var Employee = JsonConvert.DeserializeObject<EmployeeEntity>(data);

        //        string path = HttpContext.Current.Server.MapPath("~/EmployeeProfilePictures");
        //        bool folderExists = Directory.Exists(path);

        //        if (!folderExists)
        //            Directory.CreateDirectory(path);

        //        var fileName = DateTime.Now.Ticks + Path.GetExtension(profilePic.FileName);

        //        var fileSavePath = Path.Combine(path, fileName);
        //        profilePic.SaveAs(fileSavePath);

        //        if (File.Exists(fileSavePath))
        //        {
        //            Employee.ProfilePhoto = fileName;
        //        }
        //        else
        //        {
        //        }
        //        var result = _employeeServices.CreateEmployee(Employee);
        //        return Request.CreateResponse(HttpStatusCode.OK, "success!");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.NotFound);
        //    }
        //}


        //[HttpPut]
        //[Route("Modify")]
        //public bool Put([FromBody]EmployeeEntity employeeEntity)
        //{
        //    try
        //    {
        //        if (employeeEntity.CompanyId > 0)
        //        {
        //            return _employeeServices.UpdateEmployee(employeeEntity.EmployeeId, employeeEntity, employeeEntity.EmpAddress);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Employee not found", HttpStatusCode.NotFound);
        //    }
        //    return false;
        //}

        //[HttpDelete]
        //[Route("Delete/{EmployeeId}")]
        //public bool Delete(int EmployeeId)
        //{
        //    try
        //    {
        //        if (EmployeeId > 0)
        //        {
        //            var isSuccess = _employeeServices.DeleteEmployee(EmployeeId);
        //            if (isSuccess)
        //            {
        //                return isSuccess;
        //            }
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException()
        //        {
        //            ErrorCode = (int)HttpStatusCode.BadRequest,
        //            ErrorDescription = "Bad Request..."
        //        };
        //  }

      //  }

 

    }
}