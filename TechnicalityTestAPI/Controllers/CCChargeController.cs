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
        
        private readonly ICCChargeService _ccChargeService;
        
        public CCChargeController(ICCChargeService ccChargeService)
        {
            _ccChargeService = ccChargeService;
        }

        [HttpGet]
        public List<CCChargeViewModel> Get(int id)
        {
            return _ccChargeService.GetCharges(id);
        }

        [Route("[action]")]
        [HttpPost]
        public void Update(CCChargeViewModel model)
        {
            _ccChargeService.UpdateCCCharge(model.ChargeId, model.Amount);
        }


        [HttpPost]
        public int Create(CCChargeViewModel model)
        {
            return _ccChargeService.CreateCCCharge(model.CustomerId, model.Amount);
        }

    }
}
