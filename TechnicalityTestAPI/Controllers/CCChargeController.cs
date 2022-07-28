using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TechnicalityTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CCChargeController : ControllerBase
    {

        private readonly ICCChargeService _cCChargeService;

        public CCChargeController(ICCChargeService cCChargeService)
        {
            _cCChargeService = cCChargeService;
        }

        [HttpGet]
        public List<CCChargeViewModel> Get(int id)
        {
            // id is CustomerId
            return _cCChargeService.GetCharges(id);
        }

        [HttpPost]
        public int CreateCCCharge(CCChargeViewModel model)
        {
            int creditCardChargeId = _cCChargeService.CreateCCCharge(model.CustomerId, model.Amount);
            return creditCardChargeId;
        }

        [HttpPut]
        public IActionResult UpdateCCCharge(CCChargeViewModel model)
        {
            _cCChargeService.UpdateCCCharge(model.ChargeId, model.Amount);
            return Ok();
        }
    }
}
