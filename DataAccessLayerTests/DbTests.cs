using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MyModels;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace DataAccessLayer.Tests
{
    [TestClass()]
    public class DbTests
    {
        [TestMethod()]
        public void SelectFilteredEmployees_IsNOtNull()
        {
            Assert.IsNotNull(Db.SelectFilteredEmployees(new SearchModel()));
        }

        [TestMethod()]
        public void SelectFilteredEmployees_IsGetAllEmployees()
        {
            using (DataContext data = new DataContext())
            {
                string sEmpsStr = Db.SelectFilteredEmployees(new SearchModel()).ToString();
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

                string sEmpsStr = Db.SelectFilteredEmployees(model).ToString();
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

                string sEmpsStr = Db.SelectFilteredEmployees(model).ToString();
                string dEmpsStr = $"select * from Employees order by FirstName desc";

                Assert.AreEqual(sEmpsStr, dEmpsStr);
            }
        }

        [TestMethod()]
        public void EditTest_ChangeOk()
        {
            using (DataContext data = new DataContext())
            {
                Employee emp = data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();

                Employee editedEmp = Db.Edit(emp);
                Assert.IsTrue(emp.Contains(editedEmp));
            }
        }

        [TestMethod()]
        public void EditTest_AddOk()
        {
            using (DataContext data = new DataContext())
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

                Employee editedEmp = Db.Edit(emp);
                bool isAdded = emp.Contains(editedEmp);

                Employee addedEemp = data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
                Db.Remove(addedEemp);

                Assert.IsTrue(isAdded);
            }
        }

        [TestMethod()]
        public void RemoveTest_Ok()
        {
            using (DataContext data = new DataContext())
            {
                Employee emp = new Employee()
                {
                    FirstName = "test1",
                    LastName = "test1yan",
                    Age = 23,
                    Salary = 1,
                    Email = "test1@mail.ru",
                    Phone = "017111111"
                };

                Employee addedEmp = data.Employees.OrderByDescending(x => x.Id).FirstOrDefault();
                Assert.IsTrue(Db.Remove(addedEmp));
            }
        }

        [TestMethod()]
        public void GetEmployeeByIdTest_Ok()
        {
            using (DataContext data = new DataContext())
            {
                Employee emp = data.Employees.FirstOrDefault();

                Assert.IsNotNull(Db.GetEmployeeById(emp.Id));
            }
        }
    }
}