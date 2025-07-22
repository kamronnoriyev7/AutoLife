﻿namespace AutoLife.Identity.Models.AuthDTOs.Requests;


public class RegisterRequestDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
}

