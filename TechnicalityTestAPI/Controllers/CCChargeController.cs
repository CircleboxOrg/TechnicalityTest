using Microsoft.AspNetCore.Mvc;

namespace TechnicalityTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CCChargeController : ControllerBase
    {
        private readonly ICCChargeService _ccChargeService;

        public CCChargeController(ICCChargeService ccChargeService)
        {
            _ccChargeService = ccChargeService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok(_ccChargeService.GetCharges(id));
        }

        [HttpPost]
        public IActionResult CreateCCCharge(CCChargeViewModel model)
        {
            return Ok(_ccChargeService.CreateCCCharge(model));
        }
    }
}