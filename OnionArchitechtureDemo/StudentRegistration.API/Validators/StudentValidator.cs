using FluentValidation;
using StudentRegistration.Domain.Models;

namespace StudentRegistration.API.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            // Check name is not null, empty and is between 1 and 250 characters
            RuleFor(student => student.Name).NotNull().NotEmpty().Length(1, 250);

            // Validate Age for submitted student has to be between 17 and 60 years old
            RuleFor(student => student.Age).NotNull().NotEmpty().InclusiveBetween(17, 60);

            RuleFor(student => student.Emial).EmailAddress();

            // Validate Date with a custom error message
            RuleFor(student => student.JoiningDate).Must(BeAValidAge).WithMessage("Invalid Date of Joining");

            RuleFor(student => student.Address).NotNull().NotEmpty();
        }
        public bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 4))
            {
                return true;
            }

            return false;
        }
    }
}
