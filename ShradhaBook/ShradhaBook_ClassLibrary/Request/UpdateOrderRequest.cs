﻿namespace ShradhaBook_ClassLibrary.Request;

public class UpdateOrderRequest
{
    public bool Payment { get; set; }
    public string OrderTracking { get; set; } = string.Empty;
}