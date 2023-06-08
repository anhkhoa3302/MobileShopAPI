using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingaddressService;

        public ShippingAddressController(IShippingAddressService shippingaddressService)
        {
           _shippingaddressService = shippingaddressService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var addressList = await _shippingaddressService.GetAllAddressAsync();
            return Ok(addressList);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _shippingaddressService.GetByIdAsync(id);
                if (data != null)
                {
                    return Ok(data);
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
        public async Task<IActionResult> Create(ShippingAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _shippingaddressService.CreateAddressAsync(model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long addressId, ShippingAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _shippingaddressService.UpdateAddressAsync(addressId, model);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _shippingaddressService.DeleteAddressAsync(id);
                    if (result.isSuccess)
                        return Ok(result); 
                    return BadRequest(result);
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
