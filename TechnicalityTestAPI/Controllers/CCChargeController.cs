using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalityTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CCChargeController : ControllerBase
    {

        private readonly ICCChargeService _cCChargeService;

        public CCChargeController(ICCChargeService cCChargeService)
        {
            this._cCChargeService = cCChargeService;
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
            return _cCChargeService.CreateCCCharge(model.CustomerId, model.Amount);
        }

        /// <summary>
        /// Update Credit Charge
        /// </summary>
        /// <param name="updateModel"></param>
        [HttpPut]
        public void UpdateCCCharge(CChargeUpdateViewModel updateModel)
        {
            _cCChargeService.UpdateCCCharge(updateModel.ChargeId, updateModel.Amount);
        }

    }
}
