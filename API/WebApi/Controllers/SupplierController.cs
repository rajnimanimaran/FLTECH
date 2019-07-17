using BusinessEntities;
using BusinessEntities.Supplier;
using BusinessServices;
using BusinessServices.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("Supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierservice _Supplierservice;

        public SupplierController(ISupplierservice supplier)
        {
            _Supplierservice = supplier;
        }

        [HttpGet]
        [Route("AllSuppliers")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Product = _Supplierservice.GetSupplier();
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
        public bool Post([FromBody]SupplierEntity SupplierEntity)
        {
            try
            {
                return _Supplierservice.CreateSupplier(SupplierEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]SupplierEntity SupplierEntity)
        {
            try
            {
                if (SupplierEntity.SID > 0)
                {
                    return _Supplierservice.UpdateSupplier(SupplierEntity.SID, SupplierEntity);
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
                    return _Supplierservice.DeleteSupplier(id);
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
