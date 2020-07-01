using System.ComponentModel.DataAnnotations;

namespace MyModels
{
    public partial class Employee
    {
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int? Salary { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public string Email { get; set; }
        [Required]
        public int? Phone { get; set; }
    }
}
