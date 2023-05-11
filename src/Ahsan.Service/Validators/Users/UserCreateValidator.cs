using Ahsan.Domain.Entities;
using FluentValidation;

namespace Ahsan.Service.Validators.Users;

public class UserCreateValidator : AbstractValidator<Company>
{
    public UserCreateValidator()
    {
    }
}