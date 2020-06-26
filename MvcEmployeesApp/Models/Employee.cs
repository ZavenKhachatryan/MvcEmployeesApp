namespace MvcEmployeesApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        public Nullable<int> Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Nullable<int> Age { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
