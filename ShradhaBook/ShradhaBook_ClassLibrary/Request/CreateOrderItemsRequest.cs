﻿namespace ShradhaBook_ClassLibrary.Request;

public class CreateOrderItemsRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}