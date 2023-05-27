using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Services;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_colorService.GetAll());
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
                var data = _colorService.GetById(id);
                if (data != null)
                {
                    return Ok(_colorService.GetById(id));
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
        public async Task<IActionResult> Update(long id, Color color)
        {
            if (id != color.Id)
            {
                return BadRequest();
            }
            try
            {
                //_colorService.Update(color);
                //return NoContent();

                if (ModelState.IsValid)
                {
                    var result = await _colorService.Update(color);
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
                //_colorService.Delete(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _colorService.Delete(id);
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
        public async  Task<IActionResult> Add(Color color)
        {
            try
            {
                //return Ok(_colorService.Add(color));

                if (ModelState.IsValid)
                {
                    var result = await _colorService.Add(color);
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
