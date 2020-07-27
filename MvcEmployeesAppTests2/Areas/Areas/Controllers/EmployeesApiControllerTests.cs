﻿using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            OkNegotiatedContentResult<Employee> getedEmployee =
                controller.Edit(employee.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(employee.Contains(getedEmployee.Content));
        }

        [TestMethod()]
        public void C_EditTest_Get_EmptyEmployee_Ok()
        {
            int? id = null;

            OkNegotiatedContentResult<Employee> getedEmployee =
                controller.Edit(id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(new Employee().Contains(getedEmployee.Content));
        }


        [TestMethod()]
        public void D1_GetDetailsTest_Ok()
        {
            Employee emp = GetLastEmployee();

            OkNegotiatedContentResult<Employee> getById =
                controller.GetDetails(emp.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(emp.Contains(getById.Content));
        }

        [TestMethod()]
        public void D2_GetDetailsTest_IdOutOfRange_Ok()
        {
            BadRequestErrorMessageResult getById =
                controller.GetDetails(-1) as BadRequestErrorMessageResult;
            
            string getMsg = getById.Message;
            string outRangeMsg = "Wrong Id";

            Assert.IsTrue(getMsg.Contains(outRangeMsg));
        }

        [TestMethod()]
        public void E_EditTest_Post_Add_Ok()
        {
            Employee emp = CreateEmployee();

            OkNegotiatedContentResult<Employee> editedEmployee =
                controller.Edit(emp) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(emp.Contains(editedEmployee.Content));
        }

        [TestMethod()]
        public void F_EditTest_Post_AddExistException_Ok()
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
            Employee emp = GetLastEmployee();
            emp.Age = 25;

            OkNegotiatedContentResult<Employee> editedEmployee =
                controller.Edit(emp) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(emp.Contains(editedEmployee.Content));
        }


        [TestMethod()]
        public void H_RemoveTest_GetEmployee_Ok()
        {
            Employee employee = GetLastEmployee();

            OkNegotiatedContentResult<Employee> getREmp =
                controller.Remove(employee.Id) as OkNegotiatedContentResult<Employee>;

            Assert.IsTrue(employee.Contains(getREmp.Content));
        }

        [TestMethod()]
        public void I_RemoveTest_Get_IdOutOfRange_Ok()
        {
            BadRequestErrorMessageResult getREmp =
                controller.Remove(-1) as BadRequestErrorMessageResult;

            string outMsg = getREmp.Message;
            string outRangeMsg = "Wrong Id";

            Assert.IsTrue(outMsg.Contains(outRangeMsg));
        }

        [TestMethod()]
        public void J_RemoveTest_Post_()
        {
            Employee emp = GetLastEmployee();

            OkNegotiatedContentResult<bool> isRemoved =
                controller.Remove(emp) as OkNegotiatedContentResult<bool>;

            Assert.IsTrue(isRemoved.Content == true);
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