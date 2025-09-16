﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Orders.Commands.CreateOrder;
using Restaurants.Application.Orders.Commands.DeleteOrder;
using Restaurants.Application.Orders.Commands.UpdateOrder;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Application.Orders.Queries.GetAllOrders;
using Restaurants.Application.Orders.Queries.GetOrderById;
using Restaurants.Application.Orders.Queries.GetOrdersByCustomerId;
using Restaurants.Application.Orders.Queries.GetOrdersByRestaurantId;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromQuery] GetAllOrdersQuery query)
        {
            var orders = await mediator.Send(query);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.SuperAdmin},{UserRoles.User}")]
        public async Task<ActionResult<OrderDto?>> GetById([FromRoute] int id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Admin} , {UserRoles.SuperAdmin} , {UserRoles.User}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int Id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = Id;

            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Admin} , {UserRoles.SuperAdmin} , {UserRoles.User}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }

        [HttpGet("CustomerId")]
        [Authorize(Roles = $"{UserRoles.Admin} , {UserRoles.SuperAdmin} , {UserRoles.User}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByCustomerId([FromQuery] int customerId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetOrdersByCustomerIdQuery(customerId)
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("RestaurantId")]
        [Authorize(Roles = $"{UserRoles.Admin} , {UserRoles.SuperAdmin} , {UserRoles.Owner}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByRestaurantId([FromQuery] int restaurantId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection sortDirection = SortDirection.Ascending)
        {
            var query = new GetOrdersByRestaurantIdQuery(restaurantId)
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
