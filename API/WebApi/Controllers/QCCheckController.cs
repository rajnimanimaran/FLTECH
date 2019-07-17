using BusinessEntities.Master1;
using BusinessServices.Master.QualtiyCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;


namespace WebApi.Controllers
{
    [RoutePrefix("QCCheck")]
    public class QCCheckController : ApiController
    { 

        private readonly IQualityCheckService _QualityCheckService;

    public QCCheckController(IQualityCheckService QCCheck)
    {
        _QualityCheckService = QCCheck;
    }


    [HttpPost]
    [Route("AllQCCheck")]
    public HttpResponseMessage Get()
    {
        try
        {
            var Department = _QualityCheckService.GetAllQCheck();
            return Request.CreateResponse(HttpStatusCode.OK, Department);
        }
        catch (Exception ex)
        {
            throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        }
    }

    [HttpGet]
    [Route("GetQCCheckDetails/{QCID}")]
    public HttpResponseMessage GetQCCheckDetails(int QCID)
    {
        try
        {
            var Department = _QualityCheckService.GetAllQCMDetails(QCID);

            return Request.CreateResponse(HttpStatusCode.OK, Department);
        }
        catch (Exception ex)
        {
            throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        }
    }

    [HttpPost]
    [Route("Create")]
    public bool Post([FromBody]InsertQCEntity InsertQCEntity)
    {
        try
        {
            return _QualityCheckService.Create(InsertQCEntity);
        }
        catch (Exception ex)
        {
            throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
        }
    }
    [HttpPut]
    [Route("Modify")]
    public bool Put([FromBody]UpdateQCEntity UpdateQCEntity)
    {
        try
        {
            if (UpdateQCEntity.QCID > 0)
            {
                return _QualityCheckService.Update(UpdateQCEntity.QCID, UpdateQCEntity);
            }
        }
        catch (Exception ex)
        {
            throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        }
        return false;
    }
    [HttpDelete]
    [Route("Delete/{QCID}")]
    public bool Delete(int QCID)
    {
        HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        try
        {
            if (QCID > 0)
            {
                return _QualityCheckService.Delete(QCID);
            }


        }
        catch (Exception ex)
        {
            throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        }
        return false;

    }

}
}
