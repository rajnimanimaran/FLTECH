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

    [RoutePrefix("SalesOrder")]
    public class Sales_OrderController : ApiController
    {
        private readonly ISalesOrderService _SalesOrderService;

        public Sales_OrderController(ISalesOrderService sodm)
        {
            _SalesOrderService = sodm;
        }

        [HttpGet]
        [Route("AllSalesOrder/{OrderID}")]
        public HttpResponseMessage Get(int OrderID)
        {
            try
            {
                var Department = _SalesOrderService.GetAllsales(OrderID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }

        }


        [HttpPost]
        [Route("Create")]
        public bool Post(SalesOrderMaster SalesOrderMasterEntity)
        {
            try
            {
                return _SalesOrderService.Create(SalesOrderMasterEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpPut]
        [Route("Modify")]
        public bool Put(SalesOrderMaster SalesOrderMaster)
        {
            try
            {
                if (SalesOrderMaster.OrderID > 0)
                {
                    return _SalesOrderService.Update(SalesOrderMaster.OrderID, SalesOrderMaster);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }

        // Sales order Planning

        [HttpPost]
        [Route("InsertSalesOrderPlan")]
        public bool Post(SalesOrderPlanEntity SalesOrderPlanEntity)
        {
            try
            {
                return _SalesOrderService.InsertSalesOrderPlan(SalesOrderPlanEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpPut]
        [Route("UpdateSalesOrderPlan")]
        public bool Put(SalesOrderPlanEntity SalesOrderPlanEntity)
        {
            try
            {
                if (SalesOrderPlanEntity.OrderSetID > 0)
                {
                    return _SalesOrderService.UpdateSalesOrderPlan(SalesOrderPlanEntity.OrderSetID,SalesOrderPlanEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }


        [HttpGet]
        [Route("GetSalesCode")]
        public HttpResponseMessage GetSalesOrderCodeEntity()
        {
            try
            {
                var Department = _SalesOrderService.GetSalesCode();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpGet]
        [Route("GetSalesOrderPlan/{SO_RefID}")]
        public HttpResponseMessage GetPurchaseOrderByID(int SO_RefID)
        {
            try
            {
                var Department = _SalesOrderService.GetSalesOrderPlan(SO_RefID);
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }


        //[HttpDelete]
        //[Route("Remove")]
        //public bool Delete(int OrderID, int ActionBy)
        //{
        //    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        //    try
        //    {
        //        if (OrderID > 0)
        //        {
        //            return _SalesOrderService.Delete(OrderID, ActionBy);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        //    }
        //    return false;

        //}
        //[HttpPost]
        //[Route("Get")]
        //public HttpResponseMessage GetPurchaseOrderByID(int OrderID, int ActionBy)
        //{
        //    try
        //    {
        //        var Department = _SalesOrderService.get(OrderID,ActionBy);
        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}


        [HttpPost]
        [Route("InsertSupplyPlan")]
        public bool supplyplan(supply supply)
        {
            try
            {
                return _SalesOrderService.Createsupply(supply);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }



    }

}


