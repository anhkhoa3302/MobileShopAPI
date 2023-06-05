using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinPackageController : ControllerBase
    {
        private readonly ICoinPackageService _cpService;

        public CoinPackageController(ICoinPackageService cpService)
        {
            _cpService = cpService;
        }

        // api/CoinPackage/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _cpService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/CoinPackage/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var data = await _cpService.GetByIdAsync(id);
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
        // api/CoinPackage/add
        [HttpPost("add")]
        public async Task<IActionResult> Add(CoinPackageViewModel cp)
        {
            if (ModelState.IsValid)
            {
                var result = await _cpService.AddAsync(cp);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        // api/CoinPackage/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, CoinPackageViewModel cp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cpService.UpdateAsync(id, cp);
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
        // api/CoinPackage/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cpService.DeleteAsync(id);
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
