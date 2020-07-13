using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcEmployeesApp.Areas.Areas.Controllers;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MvcEmployeesApp.Areas.Areas.Controllers.Tests
{
    [TestClass()]
    public class EmployeesApiControllerTests
    {
        private readonly Mock<IDataAccess> mockDataAccess;
        private readonly IDataAccess dataAccess;

        public EmployeesApiControllerTests()
        {
            mockDataAccess = new Mock<IDataAccess>();
            dataAccess = new DataAccess(new DataContext());
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            EmployeesApiController controller = new EmployeesApiController(dataAccess);

            IHttpActionResult result = controller.GetEmployees(new SearchModel());

            OkNegotiatedContentResult<PaginationModel> okResult = result as OkNegotiatedContentResult<PaginationModel>;
            Assert.IsNotNull(okResult);
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetDetailsTest()
        {
            Assert.Fail();
        }
    }
}