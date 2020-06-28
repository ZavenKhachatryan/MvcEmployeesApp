namespace MvcEmployeesApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LoginPass
    {
        public int Id { get; set; }
        [Required]
        public string Logn { get; set; }
        [Required]
        public string Pass { get; set; }
    }
}
