using BusinessEntities.BOM_Details;
using BusinessServices.BOM_Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
     
{
    [RoutePrefix("BOM_Details")]
    public class BOM_DetailsController : ApiController
    {
        private readonly IBOM_DetailsServices _BOM_DetailsServices;

        public BOM_DetailsController(BusinessServices.BOM_Details.IBOM_DetailsServices BOM_Details)
        {
            _BOM_DetailsServices = BOM_Details;
        }


        [HttpGet]
        [Route("AllBOM_Details")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _BOM_DetailsServices.GetAllBOM_Details();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]BOM_DetailsEntity BOM_DetailsEntity)
        {
            try
            {
                return _BOM_DetailsServices.CreateBOM_Details(BOM_DetailsEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]BOM_DetailsEntity BOM_DetailsEntity)
        {
            try
            {
                if (BOM_DetailsEntity.BOMD_ID > 0)
                {

                    return _BOM_DetailsServices.UpdateBOM_Details(BOM_DetailsEntity.BOMD_ID, BOM_DetailsEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{BOMD_ID}")]
        public bool Delete(int BOMD_ID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (BOMD_ID > 0)
                {
                    return _BOM_DetailsServices.DeleteBOM_Details(BOMD_ID);
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
