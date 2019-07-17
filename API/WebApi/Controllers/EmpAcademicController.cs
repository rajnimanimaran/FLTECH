using BusinessEntities;
using BusinessServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("EmployeeAcademy")]
    public class EmpAcademicController : ApiController
    {
        private readonly IEmployeeAcademyService _empAcademyServices;

        public EmpAcademicController(IEmployeeAcademyService empAcademyServices)
        {
            _empAcademyServices = empAcademyServices;
        }


        [HttpGet]
        [Route("getAllEmpAcademicInfo/{empId}")]
        public HttpResponseMessage Get(int empId)
        {
            try
            {
                var empAcademicInfo = _empAcademyServices.GetEmpAcademyByEmpId(empId);
                return Request.CreateResponse(HttpStatusCode.OK, empAcademicInfo);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("createEmpAcademyInfo")]
        public HttpResponseMessage Post()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var academicDocs = HttpContext.Current.Request.Files;
                var data = HttpContext.Current.Request.Form["data"];

                var empAcademic = JsonConvert.DeserializeObject<EmployeeAcademyEntity>(data);

                string path = HttpContext.Current.Server.MapPath("~/EmployeeAcademy");
                bool folderExists = Directory.Exists(path);

                if (!folderExists)
                    Directory.CreateDirectory(path);

                string fileNameStr = "";
                var last = academicDocs[academicDocs.Count - 1];
                foreach (string file in academicDocs.AllKeys)
                {
                    var fileName = DateTime.Now.Ticks + Path.GetExtension(academicDocs[file].FileName);

                    var fileSavePath = Path.Combine(path, fileName);
                    academicDocs[file].SaveAs(fileSavePath);
                    if (File.Exists(fileSavePath))
                        fileNameStr += fileName;
                    if (!academicDocs[file].Equals(last))
                        fileNameStr += ",";
                }
                empAcademic.DocAttachment = fileNameStr;

                var result = _empAcademyServices.CreateEmployeeAcademy(empAcademic);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Employee Not Found", HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("removeEmpAcademyInfo/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var academicDetail = _empAcademyServices.GetEmpAcademyInfoById(id);
                if (academicDetail != null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/EmployeeAcademy");
                    bool folderExists = Directory.Exists(path);

                    if (folderExists)
                    {
                        string[] files = academicDetail.DocAttachment.Split(',');
                        foreach(var file in files)
                        {
                            var filePath = Path.Combine(path, file);
                            try
                            {
                                File.Delete(filePath);
                            }
                            catch (DirectoryNotFoundException ex)
                            {
                            }
                        }
                    }

                    var result = _empAcademyServices.DeleteEmployeeAcademy(id);
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Academic with given id is not found.");
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}
