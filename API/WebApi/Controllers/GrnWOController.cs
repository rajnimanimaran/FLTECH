using BusinessEntities.Grn;
using BusinessServices.Grn.GrnWO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("GrnWO")]
    public class GrnWOController : ApiController
    {
        private readonly IGrnWOService _GrnWOService;
        public GrnWOController (IGrnWOService GrnWOService)
        {
            _GrnWOService = GrnWOService;
        }

        [HttpPost]
        [Route("CreateWO")]
        public bool Create(GrnWOEntity obj)
        {
            try
            {
                return _GrnWOService.Create(obj);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("UpdateWO")]
        public bool Update(GrnWOUpdate obj)
        {
            try
            {
                return _GrnWOService.Update(obj);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(100, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpDelete]
        [Route("Delete/{GrnNO}")]
        public bool Delete(int GrnNO, int ActionBy)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (GrnNO > 0)
                {
                    return _GrnWOService.Delete(GrnNO, ActionBy);
                }

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpGet]
        [Route("Select/{GrnNO}")]
        public HttpResponseMessage GrnEntity(int? GrnNo)
        {
            try
            {
                var Department = _GrnWOService.select(GrnNo);

                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("AllGrnwo")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _GrnWOService.GetAllGrnwo();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetProductList")]
        public HttpResponseMessage GetProductList()
        {
            try
            {
                var Department = _GrnWOService.GetProductList();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetWOList/{WorkOrderID}")]
        public HttpResponseMessage GetWOList(int WorkOrderID)
        {
            try
            {
                var Department = _GrnWOService.GetWOList(WorkOrderID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
    }
}
