using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyModels;
using System.Linq;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        private readonly IDataAccess dataAccess;
        [TestMethod()]
        public void SelectFilteredEmployees_IsNOtNull()
        {
            Assert.IsNotNull(dataAccess.SelectFilteredEmployees(new SearchModel()));
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetAllEmployees()
        {
            using (DataContext data = new DataContext())
            {
                string sEmpsStr = dataAccess.SelectFilteredEmployees(new SearchModel()).ToString();
                string dEmpsStr = "select * from Employees";

                Assert.AreEqual(sEmpsStr, dEmpsStr);
            }
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithSearchValue()
        {
            using (DataContext data = new DataContext())
            {
                SearchModel model = new SearchModel();
                model.SearchBy = "FirstName";
                model.SearchValue = "Zaven";

                string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
                string dEmpsStr = $"select * from Employees where FirstName = 'Zaven'";

                Assert.AreEqual(sEmpsStr, dEmpsStr);
            }
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetTrueEmployees_WithOrderBy()
        {
            using (DataContext data = new DataContext())
            {
                SearchModel model = new SearchModel();
                model.OrderBy = "FirstName";
                model.AscDesc = "desc";

                string sEmpsStr = dataAccess.SelectFilteredEmployees(model).ToString();
                string dEmpsStr = $"select * from Employees order by FirstName desc";

                Assert.AreEqual(sEmpsStr, dEmpsStr);
            }
        }

        [TestMethod()]
        public void EditTest_ChangeOk()
        {
            using (DataContext data = new DataContext())
            {
                Employee addedEmp = GetLastAddedEmployee(data);
                addedEmp.Age = 24;

                Employee changedEmp = new Employee()
                {
                    FirstName = "test",
                    LastName = "testyan",
                    Age = 24,
                    Salary = 1,
                    Email = "test@mail.ru",
                    Phone = "018111111"
                };

                Employee editedEmp = dataAccess.Edit(addedEmp);
                bool isContains = editedEmp.Contains(changedEmp);
                dataAccess.Remove(addedEmp);

                Assert.IsTrue(isContains);
            }
        }
        [TestMethod()]
        public void EditTest_AddOk()
        {
            using (DataContext data = new DataContext())
            {
                Employee emp = CreateEmployee();

                Employee addedEmp = dataAccess.Edit(emp);
                bool isAdded = emp.Contains(addedEmp);

                dataAccess.Remove(addedEmp);

                Assert.IsTrue(isAdded);
            }
        }

        [TestMethod()]
        public void EditTest_Add_ExistExceptionOk()
        {
            using (DataContext data = new DataContext())
            {
                Employee emp = CreateEmployee();

                Employee addedEmp = dataAccess.Edit(emp);
                Assert.ThrowsException<ExistException>(() => dataAccess.Edit(emp));
                dataAccess.Remove(addedEmp);
            }
        }

        [TestMethod()]
        public void RemoveTest_Ok()
        {
            using (DataContext data = new DataContext())
            {
                Employee addedEmp = GetLastAddedEmployee(data);
                Assert.IsTrue(dataAccess.Remove(addedEmp));
            }
        }

        [TestMethod()]
        public void GetEmployeeByIdTest_Ok()
        {
            using (DataContext data = new DataContext())
            {
                Employee addedEmp = GetLastAddedEmployee(data);

                Employee getEmpById = dataAccess.GetEmployeeById(addedEmp.Id);
                bool isGetTrueEmployee = addedEmp.Contains(getEmpById);
                dataAccess.Remove(addedEmp);

                Assert.IsNotNull(isGetTrueEmployee);
            }
        }
        private Employee GetLastAddedEmployee(DataContext data)
        {
            Employee emp = CreateEmployee();
            Employee addedEmp = dataAccess.Edit(emp);

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