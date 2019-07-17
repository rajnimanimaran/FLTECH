using BusinessEntities.Master1.UOM;
using BusinessServices.Master1.UOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("UOMMaster")]
    public class UOMMasterController : ApiController
    {
        private readonly IUOMMasterService _UOMMasterService;

        public UOMMasterController(BusinessServices.Master1.UOM.IUOMMasterService UOM)
        {
            _UOMMasterService = UOM;
        }
        [HttpGet]
        [Route("AllUOM")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _UOMMasterService.GetAllUOM();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]UOMMasterEntity UOMEntity)
        {
            try
            {
                return _UOMMasterService.CreateUOM(UOMEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]UOMMasterEntity UOMMasterEntity)
        {
            try
            {
                if (UOMMasterEntity.UOMID > 0)
                {

                    return _UOMMasterService.UpdateUOM(UOMMasterEntity.UOMID, UOMMasterEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{UOMID}")]
        public bool Delete(int UOMID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (UOMID > 0)
                {
                    return _UOMMasterService.DeleteUOM(UOMID);
                }

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
            return false;
        }


    }
}
