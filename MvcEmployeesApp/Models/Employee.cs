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
                yield return new ValidationResult("My Error Message 1", new List<string>() { nameof(FirstName) });

            if (string.IsNullOrWhiteSpace(LastName))
                yield return new ValidationResult("My Error Message 2", new List<string>() { nameof(LastName) });

            if (string.IsNullOrWhiteSpace(Age.ToString()))
                yield return new ValidationResult("My Error Message 3", new List<string>() { nameof(Age) });

            if (string.IsNullOrWhiteSpace(Position))
                yield return new ValidationResult("My Error Message 4", new List<string>() { nameof(Position) });
        }
    }
}
