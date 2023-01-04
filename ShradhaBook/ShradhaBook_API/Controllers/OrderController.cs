using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IOrderItemsService _orderItemsService;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService,
        IOrderItemsService orderItemsService, IEmailService emailService,
        IMapper mapper)
    {
        _orderService = orderService;
        _orderItemsService = orderItemsService;
        _emailService = emailService;
        _mapper = mapper;
    }

    [HttpPost("verify")]
    public async Task<IActionResult> CheckVerify(int userId)
    {
        var isVerified = await _orderService.CheckVerify(userId);
        if (isVerified == null)
            return NotFound(new ServiceResponse<Order> { Status = false, Message = "User not found" });
        return Ok(new ServiceResponse<bool>
        {
            Data = true,
            Message = "User has been verified."
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var order = await _orderService.CreateOrder(request);
        if (order == null)
            return NotFound(new ServiceResponse<Order> { Status = false, Message = "User or Product not found" });

        var tableBody = "";
        var count = 1;
        foreach (var orderItemsRequest in request.OrderItems)
        {
            var orderItems = await _orderItemsService.CreateOrderItems(orderItemsRequest);
            if (orderItems == null)
                return NotFound(new ServiceResponse<OrderItems>
                    { Status = false, Message = "User or Product not found" });
            tableBody += $@"
                    <tr>
                    <td> {count:D2} </td>
                    <td></td>
                    <td> {orderItems.Product.Name} </td>
                    <td></td>
                    <td> {orderItems.Quantity:N} </td>
                    <td></td>
                    <td> {orderItems.UnitPrice:N} </td>
                    <td></td>
                    <td> {orderItems.Subtotal:N} </td>
                    </tr>
                    ";
            count += 1;
        }

        var emailDto = new EmailDto
        {
            To = request.Email,
            Subject = "Verify email",
            Body = @"<h3> Thank you for your purchase order. Hope to see you again.</h3><br/>
                <h4 class=""text-center"">Order Details</h4>
                <table class=""table table"">
                    <thead>
                        <tr>
                            <th>No.</th>
                            <th></th>
                            <th>Item Name</th>
                            <th></th>
                            <th>Quantity</th>
                            <th></th>
                            <th>Unit Price</th>
                            <th></th>
                            <th>Sub Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        " +
                   tableBody +
                   @"<tr>
                        <td colspan=""7"" style=""text-align: center""><b>Total</b></td>
                        <td>" + order.Total.ToString("N") + @"</td>
                    </tr>
                    </tbody>
                </table>
<a href=""#"" class=""text-align: center;"">View Order Details</a>"
        };
        _emailService.SendEmail(emailDto);
        return Ok(new ServiceResponse<Order>
        {
            Data = order,
            Message = "Order has been created successfully."
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders(string? orderNumber,
        int? productId, int? userId, string? orderTracking,
        string? paymentForms, decimal? fromTotal, decimal? toTotal,
        int page = 1, int itemPerPage = 5)
    {
        var orders = await _orderService.GetAllOrders(orderNumber, productId, userId, orderTracking,
            paymentForms, fromTotal, toTotal);

        if (orders == null) return NotFound(new ServiceResponse<Order> { Status = false, Message = "No order found." });

        var pageCount = Math.Ceiling(orders.Count / (float)itemPerPage);
        orders = orders.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        var response = new PaginationResponse<Order>
        {
            Data = orders,
            ItemPerPage = itemPerPage,
            CurrentPage = page,
            PageSize = (int)pageCount
        };
        return Ok(new ServiceResponse<PaginationResponse<Order>> { Data = response });
    }

    [HttpGet("user/{userId:int}")]
    [Authorize]
    public async Task<IActionResult> GetAllOrdersByUserId(int userId, int page = 1, int itemPerPage = 5)
    {
        var orders = await _orderService.GetAllOrdersByUserId(userId);
        if (orders == null) return NotFound(new ServiceResponse<Order> { Status = false, Message = "User not found" });

        var pageCount = Math.Ceiling(orders.Count() / (float)itemPerPage);
        orders = orders.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        var response = new PaginationResponse<Order>
        {
            Data = orders,
            ItemPerPage = itemPerPage,
            CurrentPage = page,
            PageSize = (int)pageCount
        };
        return Ok(new ServiceResponse<PaginationResponse<Order>> { Data = response });
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null) return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });

        return Ok(new ServiceResponse<Order> { Data = order });
    }

    [HttpPut("update/{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
    {
        var order = await _orderService.UpdateOrder(id, request);
        if (order == null) return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });
        return Ok(new ServiceResponse<Order>
            { Data = order, Message = "Order tracking has been updated successfully." });
    }

    [HttpPut("cancel/{id:int}")]
    [Authorize]
    public async Task<IActionResult> CancellationOrder(int id)
    {
        var order = await _orderService.CancelOrder(id);
        if (order == null) return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });
        return Ok(new ServiceResponse<Order> { Message = "Order has been cancelled successfully." });
    }
}