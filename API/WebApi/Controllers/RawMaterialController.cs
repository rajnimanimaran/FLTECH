using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("RawMaterial")]

    public class RawMaterialController : ApiController
    {
        private readonly IRawMaterialServices _rawmaterialServices;

        public RawMaterialController(IRawMaterialServices rawmaterial)
        {
            _rawmaterialServices = rawmaterial;
        }

        [HttpGet]
        [Route("AllRawMaterials")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Product = _rawmaterialServices.GetAllRawMaterials();
                return Request.CreateResponse(HttpStatusCode.OK, Product);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        //[HttpPost]
        //[Route("GetProductId/{id}")]
        //public HttpResponseMessage GetById(int id)
        //{
        //    if (id != null)
        //    {
        //        var Product = _productServices.GetProductById(id);
        //        if (Product != null)
        //            return Request.CreateResponse(HttpStatusCode.OK, Product);
        //        throw new ApiDataException(1001, "No product found for this id.", HttpStatusCode.NotFound);
        //    }
        //    throw new ApiException()
        //    {
        //        ErrorCode = (int)HttpStatusCode.BadRequest,
        //        ErrorDescription = "Bad Request..."
        //    };
        //}


        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]RawMaterialEntity rawmaterialEntity)
        {
            try
            {
                return _rawmaterialServices.CreateRawMaterial(rawmaterialEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]RawMaterialEntity rawmaterialEntity)
        {
            try
            {
                if (rawmaterialEntity.RawMaterialId > 0)
                {
                    return _rawmaterialServices.UpdateRawMaterial(rawmaterialEntity.RawMaterialId, rawmaterialEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public bool Delete(int id)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (id > 0)
                {   
                    return _rawmaterialServices.DeleteRawMaterial(id);
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
