﻿using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_ClassLibrary.Request;

public class UserLoginRequest
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}