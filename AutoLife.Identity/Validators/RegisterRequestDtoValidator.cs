using AutoLife.Identity.Models.AuthDTOs.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RegisterRequestDto = AutoLife.Identity.Models.AuthDTOs.Requests.RegisterRequestDto;

namespace AutoLife.Identity.Validators;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Foydalanuvchi nomi kerak")
            .MinimumLength(5).WithMessage("UserName kamida 5 ta belgidan iborat bo'lishi kerak");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email bo'sh bo'lmasligi kerak")
            .EmailAddress().WithMessage("Email formati noto'g'ri");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon raqam kerak")
            .Matches(@"^\+998\d{9}$").WithMessage("Telefon raqam +998 bilan boshlanishi va 9 raqamdan iborat bo'lishi kerak");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parol bo'sh bo'lmasligi kerak")
            .MinimumLength(8).WithMessage("Parol kamida 8 ta belgidan iborat bo'lishi kerak")
            .Must(HasComplexity).WithMessage("Parolda katta harf, kichik harf, raqam va maxsus belgi bo'lishi kerak");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Tug‘ilgan sana bo‘sh bo‘lmasligi kerak")
            .Must(BeAValidAge).WithMessage("Foydalanuvchi kamida 13 yoshda bo'lishi kerak");
    }

    private bool HasComplexity(string password)
    {
        return Regex.IsMatch(password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])");
    }

    private bool BeAValidAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.AddYears(age) > today) age--;
        return age >= 13;
    }
}
