using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Services;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_sizeService.GetAll());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var data = _sizeService.GetById(id);
                if (data != null)
                {
                    return Ok(_sizeService.GetById(id));
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Size size)
        {
            if (id != size.Id)
            {
                return BadRequest();
            }
            try
            {
                //_sizeService.Update(size);
                //return NoContent();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.Update(size);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                //_sizeService.Delete(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.Delete(id);
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

        [HttpPost]
        public async Task<IActionResult> Add(Size size)
        {
            try
            {
                //return Ok(_sizeService.Add(size));

                if (ModelState.IsValid)
                {
                    var result = await _sizeService.Add(size);
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
    }
}
