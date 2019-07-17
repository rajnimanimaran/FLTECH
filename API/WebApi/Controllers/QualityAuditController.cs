
using BusinessEntities;
using BusinessServices.QualityAudit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;


namespace WebApi.Controllers
{
    [RoutePrefix("QualityAudit")]

    public class QualityAuditController : ApiController
  {
        private readonly IQualityAuditService _QualityAuditService;

        public QualityAuditController(IQualityAuditService QualityAuditService)
        {
            _QualityAuditService = QualityAuditService;
        }


        [HttpGet]
        [Route("AllQCAudit")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _QualityAuditService.GetAllQCAuditDetails();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("QCACreate")]
        public bool Post(QualityAuditEntity QualityAuditEntity)
        {
            try
            {
                return _QualityAuditService.CreateQCAudit(QualityAuditEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("QCAModify")]
        public bool Put(QualityAuditEntity QualityAuditEntity)
        {
            try
            {
                if (QualityAuditEntity.QCAuditID > 0)
                {

                    return _QualityAuditService.UpdateQCAuditDetails(QualityAuditEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public bool Delete(int RMID)
        //{
        //    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        //    try
        //    {
        //        if (RMID > 0)
        //        {
        //            return _rawmaterialservices.DeleteRawmaterial(RMID);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
        //    }
        //    return false;
        //}


       



        //{
        //    private readonly IQualityAuditService _QualityAuditservice;

        //    public QualityAuditController(IQualityAuditService QulityAuditService)
        //    {
        //        _QualityAuditservice = QulityAuditService;
        //    }

        //    [HttpPost]
        //    [Route("Create")]
        //    public HttpResponseMessage Create(QualityAuditEntity QualityAuditEntity)
        //    {
        //        try
        //        {
        //            var Department = _QualityAuditservice.Create(QualityAuditEntity);
        //            return Request.CreateResponse(HttpStatusCode.OK, Department);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
        //        }
        //    }




    }
}
