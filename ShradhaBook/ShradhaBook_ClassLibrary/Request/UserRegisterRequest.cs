﻿using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_ClassLibrary.Request;

public class UserRegisterRequest
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6, ErrorMessage = "Please enter at least 6 character.")]
    public string Password { get; set; } = string.Empty;

    [Required] public string UserType { get; set; } = string.Empty;
}