using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcEmployeesApp.Models;
using MyModels;
using System.Linq;
using System.Web.Http.Results;

namespace MvcEmployeesApp.Areas.Areas.Controllers.Tests
{
    [TestClass()]
    public class EmployeesApiControllerTests
    {
        private readonly EmployeesApiController controller;
        private readonly Mock<IDataAccess> mockDataAccess;
        private readonly IDataAccess dataAccess;
        private readonly DataContext data;

        public EmployeesApiControllerTests()
        {
            mockDataAccess = new Mock<IDataAccess>();
            dataAccess = new DataAccess(new DataContext());
            controller = new EmployeesApiController(dataAccess);
            data = new DataContext();
        }

        [TestMethod()]
        public void GetEmployeesTest_Ok()
        {
            OkNegotiatedContentResult<PaginationModel> result =
                controller.GetEmployees(new SearchModel()) as OkNegotiatedContentResult<PaginationModel>;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EditTest_GetEmployeeById_Ok()
        {
            Employee employee = GetLastEmployee(data);

            OkNegotiatedContentResult<Employee> getedEmployee =
                controller.Edit(employee.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(employee.Contains(getedEmployee.Content));
        }

        [TestMethod()]
        public void EditTest_GetEmptyEmployee_Ok()
        {
            int? id = null;

            OkNegotiatedContentResult<Employee> getedEmployee =
                controller.Edit(id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(new Employee().Contains(getedEmployee.Content));
        }

        [TestMethod()]
        public void EditTest_Post()
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

        private Employee GetLastEmployee(DataContext data)
        {
            return data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}