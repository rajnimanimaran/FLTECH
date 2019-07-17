
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
using WebApi.ActionFilters;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    // [AuthorizationRequired]
    [RoutePrefix("Company")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices company)
        {
            _companyServices = company;
        }

        [HttpGet]
        [Route("AllCompany")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Company = _companyServices.GetAllCompany();
                return Request.CreateResponse(HttpStatusCode.OK, Company);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetCompanyId/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            if (id != 0)
            {
                var Company = _companyServices.GetCompanyById(id);
                if (Company != null)
                    return Request.CreateResponse(HttpStatusCode.OK, Company);
                throw new ApiDataException(1001, "No company found for this id.", HttpStatusCode.NotFound);
            }
            throw new ApiException()
            {
                ErrorCode = (int)HttpStatusCode.BadRequest,
                ErrorDescription = "Bad Request..."
            };
        }

        [HttpPost]
        [Route("FileUpload")]
        public HttpResponseMessage FileUpload()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                HttpResponseMessage message;
                var Attachment = HttpContext.Current.Request.Files["fileAttach"];
                var FileUrl = Attachment.FileName;
                if (Attachment != null && FileUrl != null)
                {
                    var pathf = HttpContext.Current.Server.MapPath("~/UploadFile/");
                    var fileSavePath = Path.Combine(pathf, FileUrl);
                    Directory.CreateDirectory(pathf);
                    Attachment.SaveAs(fileSavePath);
                }

                return message = Request.CreateResponse(HttpStatusCode.OK, new { msgText = "Success!", result = FileUrl });
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Unit Master not found", HttpStatusCode.NotFound);
            }
        }


        [HttpPost]
        [Route("Create")]
        public int Post([FromBody] CompanyEntity company)
        {
            try
            {
                //var CompanyLogo = HttpContext.Current.Request.Files["CompanyLogo"];
                //var data = HttpContext.Current.Request.Form["data"];

                //var company = JsonConvert.DeserializeObject<CompanyEntity>(data);

                //string path = HttpContext.Current.Server.MapPath("~/UploadedDocuments");
                //bool folderExists = Directory.Exists(path);
                //if (!folderExists)
                //    Directory.CreateDirectory(path);

                //var fileName = DateTime.Now.Ticks + Path.GetExtension(CompanyLogo.FileName);

                //var fileSavePath = Path.Combine(path, fileName);
                //CompanyLogo.SaveAs(fileSavePath);

                //if (File.Exists(fileSavePath))
                //{
                //    company.CompanyLogo = fileName;
                //}
                //else
                //{
                //    return -1;
                //}
                return _companyServices.CreateCompany(company);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("uploadLogo")]
        public HttpResponseMessage UploadLogo()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                var CompanyLogo = HttpContext.Current.Request.Files["CompanyLogo"];
                var CompanyId = Convert.ToInt32(HttpContext.Current.Request.Form["CompanyId"]);
                string path = HttpContext.Current.Server.MapPath("~/UploadedDocuments");
                bool folderExists = Directory.Exists(path);

                if (!folderExists)
                    Directory.CreateDirectory(path);

                var fileName = DateTime.Now.Ticks + Path.GetExtension(CompanyLogo.FileName);
                var fileSavePath = Path.Combine(path, fileName);
                CompanyLogo.SaveAs(fileSavePath);
                var cmpData = new AddLogo();

                if (File.Exists(fileSavePath))
                {
                    var cmp = _companyServices.GetCompanyById(CompanyId);
                    var fileToDelete = Path.Combine(path, cmp.CompanyLogo);
                    File.Delete(fileToDelete);
                    cmpData.CompanyId = CompanyId;
                    cmpData.CompanyLogo = fileName;
                }
                var result = _companyServices.CompanyLogoUpload(cmpData);
                return Request.CreateResponse(HttpStatusCode.OK, "success!");
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company Not Found", HttpStatusCode.NotFound);
            }
        }




        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]CompanyEntity companyEntity)
        {
            try
            {
                if (companyEntity.CompanyId > 0)
                {
                    return _companyServices.UpdateCompany(companyEntity.CompanyId, companyEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company not found", HttpStatusCode.NotFound);
            }
            return false;
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {

                if (id > 0)
                {
                    var isSuccess = _companyServices.DeleteCompany(id);
                    if (isSuccess)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.OK, isSuccess);
                    }

                }

                return msg;
            }
            catch (Exception ex)
            {
                return msg;
            }
        }

        [HttpPost]
        [Route("ChangeActive/{id}")]
        public bool DeActivate(int id)
        {
            try
            {
                if (id > 0)
                {
                    var isSuccess = _companyServices.ToggleActiveCompany(id);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company not Deactivate", HttpStatusCode.NotFound);
            }
            return true;
        }

        [HttpGet]
        [Route("GetActiveCompanyById")]
        public HttpResponseMessage GetActiveCompanyById()
        {
            try
            {
                var ObjCmp = _companyServices.GetActiveCompanyById();
                return Request.CreateResponse(HttpStatusCode.OK, ObjCmp);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Company not found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("AllOwnerShip")]
        public HttpResponseMessage GetOwnerShip()
        {
            try
            {
                var OwnerShip = _companyServices.GetAllOwnerShip();
                return Request.CreateResponse(HttpStatusCode.OK, OwnerShip);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "OwnerShip Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("AllNatureOfBusiness")]
        public HttpResponseMessage GetNatureOfBusiness()
        {
            try
            {
                var natureOfBusiness = _companyServices.GetAllNatureOfBusiness();
                return Request.CreateResponse(HttpStatusCode.OK, natureOfBusiness);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "NatureOfBusiness Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetSubCompByCompId/{companyId}")]
        public HttpResponseMessage GetSubCompByCompId(int companyId)
        {
            try
            {
                var natureOfBusiness = _companyServices.GetSubCompByCompId(companyId);
                return Request.CreateResponse(HttpStatusCode.OK, natureOfBusiness);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "NatureOfBusiness Not Found", HttpStatusCode.NotFound);
            }
        }



        //[HttpPost]
        //[Route("GetParentCompByCompId")]
        //public HttpResponseMessage GetParentCompByCompId(GetParentListDTO obj)
        //{
        //    try
        //    {
        //        var natureOfBusiness = _companyServices.GetParentCompByCompId(obj.CompanyId, obj.UserCompanyId);
        //        return Request.CreateResponse(HttpStatusCode.OK, natureOfBusiness);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Something wrong, Try again!");
        //    }
        //}
    }
}

