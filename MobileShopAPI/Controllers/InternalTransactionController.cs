using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileShopAPI.Services;
using MobileShopAPI.ViewModel;

namespace MobileShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalTransactionController : ControllerBase
    {
        private readonly IInternalTransactionService _itService;

        public InternalTransactionController(IInternalTransactionService itService)
        {
            _itService = itService;
        }

        // api/InternalTransaction/getAll
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _itService.GetAllAsync());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/InternalTransaction/getByUserId/{id}
        [HttpGet("getByUserId/{id}")]
        public async Task<IActionResult> GetByUserId(string id)
        {
            try
            {
                var data = await _itService.GetByUserIdAsync(id);
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

        // api/InternalTransaction/getBySubscriptionId/{id}
        [HttpGet("getBySubscriptionId/{id}")]
        public async Task<IActionResult> GetBySubscriptionId(int id)
        {
            try
            {
                var data = await _itService.GetBySubscriptionIdAsync(id);
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

        // api/brand/add
        [HttpPost("add")]
        public async Task<IActionResult> Add(InternalTransactionViewModel it)
        {
            if (ModelState.IsValid)
            {
                var result = await _itService.AddAsync(it);
                if (result.isSuccess)
                    return Ok(result); //Status code: 200
                return BadRequest(result);//Status code: 404
            }

            return BadRequest("Some properies are not valid");//Status code: 404
        }
    }
}
