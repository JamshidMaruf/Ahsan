using Ahsan.Domain.Entities;
using Ahsan.Service.DTOs.Companies;
using FluentValidation;

namespace Ahsan.Service.Validators.Companies;

public class CompanyCreateValidator : AbstractValidator<CompanyForCreationDto>
{
    public CompanyCreateValidator()
    {
        RuleFor(t => t.Name).NotNull().WithMessage("Name is required");
        RuleFor(t => t.OwnerId).GreaterThan(0).WithMessage("Hoyy tentak qanaqa qilib owner bolmaydi!");
    }
}
