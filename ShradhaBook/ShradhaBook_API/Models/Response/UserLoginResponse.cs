﻿namespace ShradhaBook_API.Models.Response
{
    public class UserLoginResponse
    {
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;

    }
}
