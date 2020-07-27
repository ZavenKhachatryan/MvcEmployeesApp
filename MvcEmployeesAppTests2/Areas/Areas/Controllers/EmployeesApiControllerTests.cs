using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcEmployeesApp.Models;
using MyModels;
using System;
using System.Linq;
using System.Web.Http.Results;

namespace MvcEmployeesApp.Areas.Areas.Controllers.Tests
{
    [TestClass()]
    public class EmployeesApiControllerTests
    {
        private readonly EmployeesApiController controller;
        private readonly IDataAccess dataAccess;
        private readonly DataContext data;

        public EmployeesApiControllerTests()
        {
            dataAccess = new DataAccess(new DataContext());
            data = new DataContext();
            controller = new EmployeesApiController(dataAccess);
        }


        [TestMethod()]
        public void A_GetEmployeesTest_Ok()
        {
            OkNegotiatedContentResult<PaginationModel> result =
                controller.GetEmployees(new SearchModel()) as OkNegotiatedContentResult<PaginationModel>;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void B_EditTest_Get_EmployeeById_Ok()
        {
            Employee employee = GetLastEmployee();

            OkNegotiatedContentResult<Employee> result =
                controller.Edit(employee.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(employee.Contains(result.Content));
        }

        [TestMethod()]
        public void C_EditTest_Get_EmptyEmployee_Ok()
        {
            int? id = null;

            OkNegotiatedContentResult<Employee> result =
                controller.Edit(id) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(new Employee().Contains(result.Content));
        }

        [TestMethod()]
        public void EditTest_Get_NotOk()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => controller.Edit(-1));
        }

        [TestMethod()]
        public void D1_GetDetailsTest_Ok()
        {
            Employee emp = GetLastEmployee();

            OkNegotiatedContentResult<Employee> result =
                controller.GetDetails(emp.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(emp.Contains(result.Content));
        }

        [TestMethod()]
        public void D2_GetDetailsTest_NotOk()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => controller.GetDetails(-1));
            Assert.ThrowsException<NullReferenceException>(() => controller.GetDetails(null));
        }

        [TestMethod()]
        public void E_EditTest_Post_Add_Ok()
        {
            Employee emp = CreateEmployee();

            OkNegotiatedContentResult<Employee> result =
                controller.Edit(emp) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(emp.Contains(result.Content));
        }

        [TestMethod()]
        public void F_EditTest_NotOk()
        {
            Employee emp = CreateEmployee();
            string emailAndPhoneThrowMsg = "This Email Addres And Phone Number Already Exists";
            string emailThrowMsg = "This Email Address Already Exists";
            string phoneThrowMsg = "This Phone Number Already Exists";

            BadRequestErrorMessageResult request = controller.Edit(emp) as BadRequestErrorMessageResult;
            string reqMes = request.Message;

            Assert.IsTrue(reqMes == emailAndPhoneThrowMsg || reqMes == emailThrowMsg || reqMes == phoneThrowMsg);
        }

        [TestMethod]
        public void G_EditTest_Post_Change_Ok()
        {
            Employee employee = GetLastEmployee();
            employee.Age = 25;

            OkNegotiatedContentResult<Employee> result =
                controller.Edit(employee) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(employee.Contains(result.Content));
        }


        [TestMethod()]
        public void H_RemoveTest_Get_Ok()
        {
            Employee employee = GetLastEmployee();

            OkNegotiatedContentResult<Employee> result =
                controller.Remove(employee.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsNotNull(result);
            Assert.IsTrue(employee.Id == result.Content.Id);
        }

        [TestMethod()]
        public void RemoveTest_NotOk()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => controller.Remove(-1));
            Assert.ThrowsException<NullReferenceException>(() => controller.Remove((int?)null));
        }

        [TestMethod()]
        public void J_RemoveTest_Post_Ok()
        {
            Employee emp = GetLastEmployee();

            OkNegotiatedContentResult<bool> result =
                controller.Remove(emp) as OkNegotiatedContentResult<bool>;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content == true);
        }

        private Employee GetLastEmployee()
        {
            return data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        private Employee CreateEmployee()
        {
            Employee emp = new Employee()
            {
                FirstName = "test",
                LastName = "testyan",
                Age = 23,
                Salary = 1,
                Email = "test@mail.ru",
                Phone = "018111111"
            };

            return emp;
        }
    }
}