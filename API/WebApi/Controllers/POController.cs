using BusinessEntities;
using BusinessEntities.Master1.SalesOrder;
using BusinessServices.Master1.POMaster;
using System;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;

namespace WebApi.Controllers
{
    [RoutePrefix("PO")]
    public class POController : ApiController
    {
        private readonly IPOMasterService _POMasterService;

        public POController(IPOMasterService sodm)
        {
            _POMasterService = sodm;
        }


        [HttpGet]
        [Route("AllPO")]
        public HttpResponseMessage Get()
        {
            try
            {
                var Department = _POMasterService.GetAllsodm();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("AllSupplierName")]
        public HttpResponseMessage GetSupplierName()
        {
            try
            {
                var Department = _POMasterService.GetAllSupplierName();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("getReportpurchaseId")]
        public HttpResponseMessage Getreport()
        {
            try
            {
                var Department = _POMasterService.Getreportpurchase();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("getReportSalesId")]
        public HttpResponseMessage GetSalesreportID()
        {
            try
            {
                var Department = _POMasterService.SalesGetReportID();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetPoDetails")]
        public HttpResponseMessage Getpodetails(int PurchaseID)
        {
            try
            {
                var Department = _POMasterService.GetPurchaseOrderDetails(PurchaseID);

                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]PurchaseOrderCommonEntity PurchaseOrderCommonEntity)
        {
            try
            {
                return _POMasterService.Create(PurchaseOrderCommonEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]PurchaseOrderCommonEntity purchaseMDEntity)
        {
            try
            {
                if (purchaseMDEntity.PurchaseID > 0)
                {
                    return _POMasterService.Update(purchaseMDEntity.PurchaseID, purchaseMDEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        //[HttpDelete]
        //[Route("Delete/{ID}/{PurchaseID}")]
        //public bool Delete(int ID, int PurchaseID)
        //{
        //    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
        //    try
        //    {
        //        if (ID > 0)
        //        {
        //            return _POMasterService.Delete(ID, PurchaseID);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
        //    }
        //    return false;


        //}

        [HttpDelete]
        [Route("Delete/{ID}")]
        public bool Delete(int ID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (ID > 0)
                {
                    return _POMasterService.Delete(ID);
                }

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;

        }



        [HttpGet]
        [Route("AllSO")]
        public HttpResponseMessage GetSo()
        {
            try
            {
                var Department = _POMasterService.GetAllpodm();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }


        [HttpGet]
        [Route("GetSoDetails")]
        public HttpResponseMessage Getsodetails(int OrderID)
        {
            try
            {
                var Department = _POMasterService.GetSalesOrderDetails(OrderID);

                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        //[HttpGet]
        //[Route("AllSupplierName")]
        //public HttpResponseMessage GetSupplierName()
        //{
        //    try
        //    {
        //        var Department = _SalesOrderService.GetAllSupplierName();
        //        return Request.CreateResponse(HttpStatusCode.OK, Department);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
        //    }
        //}
        [HttpPost]
        [Route("Creates")]
        public bool Post([FromBody]SalesOrderCommonEntity SalesOrderCommonEntity)
        {
            try
            {
                return _POMasterService.Create(SalesOrderCommonEntity);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("Modifys")]
        public bool Put([FromBody]SalesOrderCommonEntity SalesOrderCommonEntity)
        {
            try
            {
                if (SalesOrderCommonEntity.OrderID > 0)
                {
                    return _POMasterService.Update(SalesOrderCommonEntity.OrderID, SalesOrderCommonEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;
        }

        [HttpDelete]
        [Route("Deletes/{SOID}")]
        public bool DeleteS(int SOID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (SOID > 0)
                {
                    return _POMasterService.DeleteS(SOID);
                }

            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Product not found", HttpStatusCode.NotFound);
            }
            return false;

        }



        [HttpPost]
        [Route("ReportView/{id}")]
        public HttpResponseMessage ReportView(int id, PurchaseGetReportEntity obj)
        {
            HttpResponseMessage message;
            try
            {


                //dsRptInvoice

                //DalLayer dal = new DalLayer();
                var result = _POMasterService.ReturnPurchaseList(id,obj);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";
                rds.Value = result.Tables[0];

                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = "DataSet2";
                rds1.Value = result.Tables[1];





                ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/report/PurchaseReport.rdlc");

                // Add the new report datasource to the report.
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.DataSources.Add(rds1);

                rv.LocalReport.Refresh();
                //rv.LocalReport.

                byte[] streamBytes = null;
                string mimeType = "";
                string encoding = "";
                string filenameExtension = "";
                string[] streamids = null;
                Warning[] warnings = null;

                streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                string filename = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
                string locationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/report/");
                using (FileStream stream = new FileStream(locationPath + filename + ".pdf", FileMode.Create))
                {
                    stream.Write(streamBytes, 0, streamBytes.Length);
                }
                ResultPurchase rs = new ResultPurchase();
                rs.data = result;
                rs.path = filename + ".pdf";
                message = Request.CreateResponse(HttpStatusCode.OK, rs);

            }
            catch (Exception ex)

            {
                //_invoice.errorLog(ex.Message.ToString());
                message = Request.CreateResponse(HttpStatusCode.BadRequest, new { msgText = "Something wrong. Try Again!" });
            }
            return message;


        }

        [HttpPost]
        [Route("SalesReportView/{id}")]
        public HttpResponseMessage SalesReportView(int id, SalesGetReportEntity obj)
        {
            HttpResponseMessage message;
            try
            {


                //dsRptInvoice

                //DalLayer dal = new DalLayer();
                var result = _POMasterService.ReturnSalesList(id, obj);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet3";
                rds.Value = result.Tables[0];

                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = "DataSet4";
                rds1.Value = result.Tables[1];





                ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/report/SalesReport.rdlc");

                // Add the new report datasource to the report.
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.DataSources.Add(rds1);

                rv.LocalReport.Refresh();
                //rv.LocalReport.

                byte[] streamBytes = null;
                string mimeType = "";
                string encoding = "";
                string filenameExtension = "";
                string[] streamids = null;
                Warning[] warnings = null;

                streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                string filename = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
                string locationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/report/");
                using (FileStream stream = new FileStream(locationPath + filename + ".pdf", FileMode.Create))
                {
                    stream.Write(streamBytes, 0, streamBytes.Length);
                }
                ResultSales rs = new ResultSales();
                rs.data = result;
                rs.path = filename + ".pdf";
                message = Request.CreateResponse(HttpStatusCode.OK, rs);

            }
            catch (Exception ex)

            {
                //_invoice.errorLog(ex.Message.ToString());
                message = Request.CreateResponse(HttpStatusCode.BadRequest, new { msgText = "Something wrong. Try Again!" });
            }
            return message;


        }

    }
}
