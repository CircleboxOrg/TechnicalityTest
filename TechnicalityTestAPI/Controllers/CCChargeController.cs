using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalityTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CCChargeController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CCChargeController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CCChargeViewModel> Get(int id)
        {
            // id is CustomerId

            return _context.CreditCardCharges.Where(c => c.CustomerId == id).Select(c =>
                new CCChargeViewModel { CustomerId = c.CustomerId, Amount = c.Amount }).ToList();
        }

        [HttpPost]
        public int CreateCCCharge(CCChargeViewModel model)
        {
            var ccc = new CreditCardCharge
            {
                CreditCardChargeId = model.ChargeId,
                CustomerId = model.CustomerId,
                Amount = model.Amount,
                ChargeDateTime = DateTime.UtcNow
            };

            _context.CreditCardCharges.Add(ccc);
            _context.SaveChanges();

            return ccc.CreditCardChargeId;
        }
    }
}