using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Create(QualityAuditEntity obj)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
            //try
            //{
            //    var Department = _QulityAuditservice.Create(QualityAuditEntity);
            //    return Request.CreateResponse(HttpStatusCode.OK, Department);
            //}
            //catch (Exception ex)
            //{
            //    throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            //}
        }
    }
}
