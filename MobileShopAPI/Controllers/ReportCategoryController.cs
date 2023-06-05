using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportCategoryController : ControllerBase
    {
        private readonly IReportCategoryService _categoryService;

        public ReportCategoryController(IReportCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // api/reportcategory/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _categoryService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/reportcategory/getById/{id}
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await _categoryService.GetByIdAsync(id);
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
        // api/reportcategory/add
        [HttpPost("add")]
        public async Task<IActionResult> Add(ReportCategoryViewModel reportCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(reportCategory);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
        // api/reportcategory/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, ReportCategoryViewModel reportCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.UpdateAsync(id, reportCategory);
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
        // api/reportcategory/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryService.DeleteAsync(id);
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
