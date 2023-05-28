using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Services;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _cateService;

        public CategoryController(ICategoryService cateService)
        {
            _cateService = cateService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_cateService.GetAll());
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
                var data = _cateService.GetById(id);
                if (data != null)
                {
                    return Ok(_cateService.GetById(id));
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
        public async Task<IActionResult> Update(long id, Category cate)
        {
            if (id != cate.Id)
            {
                return BadRequest();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cateService.Update(cate);
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
                //_cateService.Delete(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _cateService.Delete(id);
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
        public async Task<IActionResult> Add(Category cate)
        {
            try
            {
                //return Ok(_cateService.Add(cate));
                if (ModelState.IsValid)
                {
                    var result = await _cateService.Add(cate);
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
