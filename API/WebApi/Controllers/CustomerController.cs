using BusinessEntities.Master1.Customer;
using BusinessServices.Master1.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("Customer")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _CustomerService;

        public CustomerController(ICustomerService Customer)
        {
            _CustomerService = Customer;

        }

        [HttpGet]
        [Route("AllCustomer")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Customer = _CustomerService.GetAllCustomer();
                return Request.CreateResponse(HttpStatusCode.OK, Customer);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]CustomerEntity CustomerEntity)
        {
            try
            {
                return _CustomerService.Create(CustomerEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product Not Found", HttpStatusCode.NotFound);
            }
        }




        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]CustomerEntity CustomerEntity)
        {
            try
            {
                if (CustomerEntity.CustId > 0)
                {
                    return _CustomerService.Update(CustomerEntity.CustId, CustomerEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{CustId}")]
        public bool Delete(int CustId)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (CustId > 0)
                {
                    return _CustomerService.Delete(CustId);
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
