using Exceptions;
using System;
using System.Linq;

namespace MyModels
{
    public static class EmpExtensions
    {
        public static bool Contains(this Employee thisEmp, Employee emp)
        {
            return thisEmp.FirstName == emp.FirstName && thisEmp.LastName == emp.LastName && thisEmp.Age == emp.Age && thisEmp.Salary == emp.Salary && thisEmp.Email == emp.Email && thisEmp.Phone == emp.Phone;
        }
    }
}
