using AutoLife.Identity.Models.AuthDTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Validators;

public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
{
    public RefreshTokenRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token bo'sh bo'lmasligi kerak")
            .MinimumLength(20).WithMessage("Refresh token juda qisqa ko‘rinmoqda");

        RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access token bo'sh bo'lmasligi kerak")
            .MinimumLength(20).WithMessage("Access token juda qisqa ko‘rinmoqda");
    }
}