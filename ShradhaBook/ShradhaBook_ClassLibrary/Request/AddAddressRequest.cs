﻿namespace ShradhaBook_ClassLibrary.Request;

public class AddAddressRequest
{
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int? Postcode { get; set; }
    public string Country { get; set; } = string.Empty;
    public int UserInfoId { get; set; }
}