namespace MyModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public int? Salary { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
    }
}
