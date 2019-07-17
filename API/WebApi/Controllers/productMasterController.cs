using BusinessEntities.Master1.product;
using BusinessServices.Master1.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.ErrorHelper;
using BusinessEntities;

namespace WebApi.Controllers
{
    [RoutePrefix("ProductMaster")]
    public class productMasterController : ApiController
    {

        private readonly IproductService _productService;

        public productMasterController(BusinessServices.Master1.Product.IproductService product)
        {
            _productService = product;
        }


        [HttpGet]
        [Route("Allproduct")]
        public HttpResponseMessage Get()
        {
            try
            {
                List<StudentDetails> lstStu = new List<StudentDetails>();
                StudentDetails stu = new StudentDetails
                {
                    StudentId=1,
                    StudentName="XXX",
                };
                lstStu.Add(stu);
                stu = new StudentDetails
                {
                    StudentId = 2,
                    StudentName = "YYY"
                };
                lstStu.Add(stu);
                stu = new StudentDetails
                {
                    StudentId = 3,
                    StudentName = "XXX"
                };
                lstStu.Add(stu);
                List<StudentDetails> lstNew = lstStu.Where(w => w.StudentName == "XXX").ToList();
                StudentDetails studentDetails = lstStu.Where(w => w.StudentId == 2).SingleOrDefault();
                var stuId = lstStu.GroupBy(g => g.StudentName).Select(s => s.Min(x => x.StudentId));


                var Department = _productService.GetAllproduct();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("GetUOMMaster")]
        public HttpResponseMessage GetUOMMaster()
        {
            try
            {
                var Department = _productService.GetUOMDetails();
                return Request.CreateResponse(HttpStatusCode.OK, Department);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Department Not Found", HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("GetproductMaster/{prdID}")]
        public HttpResponseMessage GetProductMaster(int prdID)
        {
            try
            {
                var product = _productService.GetProductMaster(prdID);
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Role Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPost]
        [Route("Create")]
        public bool Post([FromBody]productEntity obj)
        {
            try
            {
                return _productService.Createproduct(obj);
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category Not Found", HttpStatusCode.NotFound);
            }
        }
        [HttpPut]
        [Route("Modify")]
        public bool Put([FromBody]productEntity productEntity)
        {
            try
            {
                if (productEntity.prdID > 0)
                {

                    return _productService.Updateproduct(productEntity.prdID, productEntity);
                }
            }
            catch (Exception ex)
            {
                throw new ApiDataException(1000, "Category not found", HttpStatusCode.NotFound);
            }
            return false;
        }
        [HttpDelete]
        [Route("Delete/{prdID}")]
        public bool Delete(int prdID)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.BadRequest, false);
            try
            {
                if (prdID > 0)
                {
                    return _productService.Deleteproduct(prdID);
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
