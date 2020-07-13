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
        private readonly IDataAccess dataAccess;
        private readonly IDataAccess fakeDataAccess;
        private readonly DataContext data;

        public DataAccessTests()
        {
            data = new DataContext();
            dataAccess = new DataAccess(new DataContext());
            fakeDataAccess = new DataAccess(new DataContext(ConfigurationManager.ConnectionStrings["FakeData"].ConnectionString));
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsNOtNull()
        {
            Assert.IsNotNull(dataAccess.SelectFilteredEmployees(new SearchModel()));
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetAllEmployees()
        {
            string sEmpsStr = dataAccess.SelectFilteredEmployees(new SearchModel()).ToString();
            string dEmpsStr = "select * from Employees";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithSearchValue()
        {
            SearchModel model = new SearchModel();
            model.SearchBy = "FirstName";
            model.SearchValue = "Zaven";

            string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
            string dEmpsStr = $"select * from Employees where FirstName = 'Zaven'";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithOrderBy()
        {
            SearchModel model = new SearchModel();
            model.OrderBy = "FirstName";
            model.AscDesc = "desc";

            string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
            string dEmpsStr = $"select * from Employees order by FirstName desc";

            Assert.AreEqual(sEmpsStr, dEmpsStr);
        }

        [TestMethod()]
        public void EditTest_DatabaseException()
        {
            Assert.ThrowsException<DatabaseException>(() => fakeDataAccess.Edit(new Employee()));
        }

        [TestMethod()]
        public void EditTest_AddOk()
        {
            Employee emp = CreateEmployee();

            Employee addedEmp = dataAccess.Edit(emp);

            Assert.IsTrue(emp.Contains(addedEmp));
        }

        [TestMethod()]
        public void EditTest_Add_ExistExceptionOk()
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
        public void GetEmployeeByIdTest_DatabaseException()
        {
            Assert.ThrowsException<DatabaseException>(() => fakeDataAccess.GetEmployeeById(new Employee().Id));
        }

        [TestMethod()]
        public void GetEmployeeByIdTest_IdOutOfRangeException_Ok()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => dataAccess.GetEmployeeById(-1));
        }

        [TestMethod()]
        public void EditTest_ChangeOk()
        {
            Employee lastEmp = GetLastEmployee();
            lastEmp.Age = 56;

            Employee editedEmp = dataAccess.Edit(lastEmp);
            bool isContains = editedEmp.Contains(lastEmp);

            Assert.IsTrue(isContains);
        }

        [TestMethod()]
        public void RemoveTest_DatabaseException()
        {
            Assert.ThrowsException<DatabaseException>(() => fakeDataAccess.Remove(new Employee()));
        }

        [TestMethod()]
        public void RemoveTest_Ok()
        {
            Employee lastEmp = GetLastEmployee();
            Assert.IsTrue(dataAccess.Remove(lastEmp));
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

        public void Dispose()
        {
            data.Dispose();
        }
    }
}