using FluentValidation;
using No5.Dtos.Employee;

namespace No5.Validators
{
    public class EmployeePostValidation:AbstractValidator<EmployeePostDto>
    {
        public EmployeePostValidation()
        {
            RuleFor(e=>e.Name).NotEmpty().NotNull();
            RuleFor(e=>e.Position).NotEmpty().NotNull();
            RuleFor(e=>e.About).NotEmpty().NotNull();
            RuleFor(e=>e.Icons).NotEmpty().NotNull();
        }
    }
}
