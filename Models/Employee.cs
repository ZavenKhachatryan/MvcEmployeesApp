namespace MyModels
{
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]?[0-9]{1}$|^100$")]
        public int? Age { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]{2})([0-9]{6})$")]
        public string Phone { get; set; }
    }
}
