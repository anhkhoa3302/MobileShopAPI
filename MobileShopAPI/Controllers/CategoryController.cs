using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Models;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _cateService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _cateService.GetByIdAsync(id);
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
        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryViewModel cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cateService.AddAsync(cate);
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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, CategoryViewModel cate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cateService.UpdateAsync(id,cate);
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                //_cateService.DeleteAsync(id);
                //return Ok();

                if (ModelState.IsValid)
                {
                    var result = await _cateService.DeleteAsync(id);
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
