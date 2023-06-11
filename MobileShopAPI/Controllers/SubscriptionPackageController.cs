using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;
using System.Security.Claims;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPackageController : ControllerBase
    {
        private readonly ISubscriptionPackageService _spService;

        public SubscriptionPackageController(ISubscriptionPackageService spService)
        {
            _spService = spService;
        }

        // api/SubscriptionPackage/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _spService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/SubscriptionPackage/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _spService.GetByIdAsync(id);
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
        // api/SubscriptionPackage/add
        [HttpPost("add")]
        public async Task<IActionResult> Add(SubscriptionPackageViewModel sp)
        {
            if (ModelState.IsValid)
            {
                var result = await _spService.AddAsync(sp);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        // api/SubscriptionPackage/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, SubscriptionPackageViewModel sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.UpdateAsync(id, sp);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Some properies are not valid");//Status code: 404
            }
        }

        // api/SubscriptionPackage/updateStatus/{id}
        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(long id, SubscriptionPackageStatusViewModel sp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.UpdateStatusAsync(id, sp);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Some properies are not valid");//Status code: 404
            }
        }

        // api/SubscriptionPackage/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _spService.DeleteAsync(id);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
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
