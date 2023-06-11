using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Models;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var ordeList = await _orderService.GetAllOrderAsync();
            return Ok(ordeList);
        }

        [HttpGet("getListOrderByUser")]
        public async Task<IActionResult> GetListByUser(string id)
        {
            var ordeList = await _orderService.GetListOrderByUser(id);
            return Ok(ordeList);
        }

        [HttpGet("GetOrderDetail")]
        public async Task<IActionResult> GetById(string id)
        { 
            try
            {
                var order = await _orderService.GetOrderDetailAsync(id);
                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getListBuyerByUser")]
        public async Task<IActionResult> GetListBuyerByUser(string id)
        {
            
            try
            {
               var ordeList = await _orderService.GetListBuyerByUser(id);
                if (ordeList != null)
                {
                    return Ok(ordeList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.CreateOrderAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPut("edit/{Id}")]
        public async Task<IActionResult> Update(string id, OrderUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.UpdateOrderAsync(id, model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.DeleteOrderAsync(id);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
