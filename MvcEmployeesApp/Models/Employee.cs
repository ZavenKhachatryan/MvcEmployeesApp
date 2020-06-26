namespace MvcEmployeesApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee : IValidatableObject
    {
        public Nullable<int> Id { get; set; }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        //[Required]
        public Nullable<int> Age { get; set; }
        //[Required]
        public string Position { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                yield return new ValidationResult("My Error Message 1", new List<string>() { nameof(FirstName), nameof(LastName), nameof(Age), nameof(Position) });
        }
    }
}
