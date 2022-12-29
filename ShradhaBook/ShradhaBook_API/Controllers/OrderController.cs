﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Models.Entities;
using ShradhaBook_API.Models.Response;
using ShradhaBook_API.Services.OrderItemsService;
using ShradhaBook_API.Services.OrderService;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemsService _orderItemsService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, 
            IOrderItemsService orderItemsService, IEmailService emailService,
            IMapper mapper)
        {
            _orderService = orderService;
            _orderItemsService = orderItemsService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var order = await _orderService.CreateOrder(request);
            if (order == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "User or Product not found" });
            }

            string tableBody = "";
            int count = 1;
            foreach (CreateOrderItemsRequest orderItemsRequest in request.OrderItems)
            {
                var orderItems = await _orderItemsService.CreateOrderItems(orderItemsRequest);
                if (orderItems == null)
                {
                    return NotFound(new ServiceResponse<OrderItems> { Status = false, Message = "User or Product not found" });
                }
                tableBody += $@"
                    <tr>
                    <td> {count:D2} </td>
                    <td> {orderItems.Product.Name} </td>
                    <td> {orderItems.Quantity} </td>
                    <td> {orderItems.UnitPrice} </td>
                    <td> {orderItems.Subtotal} </td>
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
                            <th>Item Name</th>
                            <th>Quantity</th>
                            <th>Unit Price</th>
                            <th>Sub Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        " +
                            tableBody +
                    @"</tbody>
                </table>"
            };
            _emailService.SendEmail(emailDto);
            return Ok(new ServiceResponse<Order>
            {
                Data = order,
                Message = "Order has been created successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(string? orderNumber,
            int? productId, int? userId, string? orderTracking,
            string? paymentForms, decimal? fromTotal, decimal? toTotal,
            int page = 1, int itemPerPage = 5)
        {
            var orders = await _orderService.GetAllOrders(orderNumber, productId, userId, orderTracking, 
                paymentForms, fromTotal, toTotal);

            if (orders == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "No order found." });
            }

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllOrdersByUserId(int userId, int page = 1, int itemPerPage = 5)
        {
            var orders = await _orderService.GetAllOrdersByUserId(userId);
            if (orders == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "User not found" });
            }

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });
            }

            return Ok(new ServiceResponse<Order> { Data = order });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = await _orderService.UpdateOrder(id, request);
            if (order == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });
            }
            return Ok(new ServiceResponse<Order> { Data = order, Message = "Order tracking has been updated successfully." });
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancellationOrder(int id)
        {
            var order = await _orderService.CancelOrder(id);
            if (order == null)
            {
                return NotFound(new ServiceResponse<Order> { Status = false, Message = "Order not found" });
            }
            return Ok(new ServiceResponse<Order> { Message = "Order has been cancelled successfully." });
        }
    }
}
