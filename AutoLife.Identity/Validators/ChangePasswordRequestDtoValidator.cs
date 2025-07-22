using AutoLife.Identity.Models.AuthDTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoLife.Identity.Validators;

public class ChangePasswordRequestDtoValidator : AbstractValidator<ChangePasswordRequestDto>
{
    public ChangePasswordRequestDtoValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Eski parolni kiritish shart")
            .MinimumLength(6).WithMessage("Eski parol kamida 6 ta belgidan iborat bo'lishi kerak");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yangi parolni kiritish shart")
            .MinimumLength(8).WithMessage("Yangi parol kamida 8 ta belgidan iborat bo'lishi kerak")
            .Must(HasComplexity).WithMessage("Yangi parolda katta harf, kichik harf, raqam va maxsus belgi bo'lishi kerak");

        RuleFor(x => x)
            .Must(x => x.OldPassword != x.NewPassword)
            .WithMessage("Yangi parol eski paroldan farqli bo'lishi kerak");
    }

    private bool HasComplexity(string password)
    {
        // Kamida: 1 kichik, 1 katta, 1 raqam, 1 maxsus belgi
        return Regex.IsMatch(password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9])");
    }
}
