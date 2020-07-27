using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyModels;
using System;
using System.Configuration;
using System.Linq;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class DataAccessTests : IDisposable
    {
        private readonly DataContext data;
        private readonly IEmployeeDataAccess dataAccess;

        public DataAccessTests()
        {
            data = new DataContext(ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString);
            dataAccess = new EmployeeDataAccess(data);
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsNotNull_Ok()
        {
            Assert.IsNotNull(dataAccess.SelectFilteredEmployees(new SearchModel()));
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetAllEmployees_Ok()
        {
            string sEmpsStr = dataAccess.SelectFilteredEmployees(new SearchModel()).ToString();
            string dEmpsStr = "select * from Employees";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithSearchValue_Ok()
        {
            SearchModel model = new SearchModel();
            model.SearchBy = "FirstName";
            model.SearchValue = "Zaven";

            string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
            string dEmpsStr = $"select * from Employees where FirstName = 'Zaven'";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithOrderBy_Ok()
        {
            SearchModel model = new SearchModel();
            model.OrderBy = "FirstName";
            model.AscDesc = "desc";

            string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
            string dEmpsStr = $"select * from Employees order by FirstName desc";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void EditTest_A_Add_Ok()
        {
            Employee emp = CreateEmployee();

            Employee addedEmp = dataAccess.Edit(emp);

            Assert.IsNotNull(addedEmp);
            Assert.IsTrue(emp.Contains(addedEmp));
        }

        [TestMethod()]
        public void EditTest_B_Add_NotOk()
        {
            Employee emp = CreateEmployee();

            Assert.ThrowsException<ExistException>(() => dataAccess.Edit(emp));
        }

        [TestMethod()]
        public void GetEmployeeByIdTest_Ok()
        {
            Employee lastEmp = GetLastEmployee();

            Employee getEmpById = dataAccess.GetEmployeeById(lastEmp.Id);

            Assert.IsNotNull(lastEmp.Contains(getEmpById));
        }

        [TestMethod()]
        public void GetEmployeeByIdTest_NotOk()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dataAccess.GetEmployeeById(-1));
        }

        [TestMethod()]
        public void EditTest_Change_Ok()
        {
            Employee lastEmp = GetLastEmployee();
            lastEmp.Age = 56;

            Employee editedEmp = dataAccess.Edit(lastEmp);
            bool isContains = editedEmp.Contains(lastEmp);

            Assert.IsNotNull(editedEmp);
            Assert.IsTrue(isContains);
        }

        [TestMethod()]
        public void EditTest_Change_NotOk()
        {
            Employee firstEmp = GetFirstEmployee();
            firstEmp.Email = CreateEmployee().Email;
            firstEmp.Phone = CreateEmployee().Phone;

            Assert.ThrowsException<ExistException>(() => dataAccess.Edit(firstEmp));
        }



        [TestMethod()]
        public void RemoveTest_Ok()
        {
            Employee lastEmp = GetLastEmployee();
            bool result = dataAccess.Remove(lastEmp);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void RemoveTest_NotOk()
        {
            Employee emp = new Employee();

            emp.Id = -1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dataAccess.Remove(emp));
            Assert.ThrowsException<NullReferenceException>(() => dataAccess.Remove(null));
        }

        private Employee GetLastEmployee()
        {
            return data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
        }
        private Employee GetFirstEmployee()
        {
            return data.Employees.OrderBy(x => x.Id).FirstOrDefault();
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

        public void Dispose()
        {
            data.Dispose();
        }
    }
}