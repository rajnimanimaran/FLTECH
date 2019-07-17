using BusinessEntities.Grn;
using BusinessServices.Grn.GrnPO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("FlowGRNPO")]
    public class GrnPOController : ApiController
    {
        private readonly IGrnPOService _GrnPOService;

            public GrnPOController(IGrnPOService GrnPOService)
            {
                _GrnPOService = GrnPOService;
            }

            //[HttpPost]
            //[Route("CreatePO")]
            //public bool Create(GrnEntity GrnEntity)
            //{
            //    try
            //    {
            //        return _GrnPOService.Create(GrnEntity);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            //    }
            //}
            [HttpPut]
            [Route("UpdatePO")]
            public bool Update(GrnPOUpdate obj)
        {
                try
                {
                    return _GrnPOService.Update(obj);
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
                        return _GrnPOService.Delete(GrnNO, ActionBy);
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
                    var Department = _GrnPOService.select(GrnNo);

                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
                }
            }

            [HttpGet]
            [Route("AllGrn")]
            public HttpResponseMessage Get()
            {
                try
                {
                    var Department = _GrnPOService.GetAllGrn();
                    return Request.CreateResponse(HttpStatusCode.OK, Department);
                }
                catch (Exception ex)
                {
                    throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
                }
            }


        [HttpGet]
        [Route("AllGrnRM")]
        public HttpResponseMessage getRM()
        {
            try
            {
                var Department = _GrnPOService.GetAllRM();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("AllGrnPrd")]
        public HttpResponseMessage getPrd()
        {
            try
            {
                var Department = _GrnPOService.GetAllPrd();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }




        [HttpPost]
        [Route("SaveGRNPO")]
        public bool Post(SaveGRNEntity GRNMasterEntity)
        {
            try
            {
                return _GrnPOService.SaveGRNPO(GRNMasterEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }

    }
    }
