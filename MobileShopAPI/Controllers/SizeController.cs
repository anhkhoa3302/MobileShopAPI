using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        // api/Size/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _sizeService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // api/Size/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _sizeService.GetByIdAsync(id);
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
        // api/Size/add
        [HttpPost("add")]
        public async Task<IActionResult> Add(SizeViewModel size)
        {
            try
            {
                //return Ok(_sizeService.AddImageAsync(size));

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.AddAsync(size);
                    if (result.isSuccess)
                        return Ok(result); //Status code: 200
                    return BadRequest(result);//Status code: 404
                }

                return BadRequest("Some properies are not valid");//Status code: 404
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // api/Size/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, SizeViewModel size)
        {
            try
            {
                //_sizeService.UpdateAsync(size);
                //return NoContent();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.UpdateAsync(id,size);
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
        // api/Size/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                //_sizeService.DeleteAsync(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.DeleteAsync(id);
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
