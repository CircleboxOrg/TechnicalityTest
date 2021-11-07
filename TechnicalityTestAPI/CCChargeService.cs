using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalityTestAPI
{
    public class CCChargeService : ICCChargeService
    {
        private readonly ICCChargeRepository _repository;

        public CCChargeService(ICCChargeRepository repository)
        {
            _repository = repository;
        }

        public List<CCChargeViewModel> GetCharges(int customerId)
        {
            var list = new List<CCChargeViewModel>();

            var charges = _repository.GetCharges(customerId); 
            return charges.Select(x => new CCChargeViewModel
            {
                ChargeId = x.CreditCardChargeId,
                CustomerId = x.CustomerId,
                Amount = x.Amount
            }).ToList();
        }

        public int CreateCCCharge(CCChargeViewModel model)
        {
            var creditCardCharge = new CreditCardCharge
            {
                CreditCardChargeId = model.ChargeId,
                CustomerId = model.CustomerId,
                Amount = model.Amount,
                ChargeDateTime = DateTime.UtcNow
            };

            return _repository.CreateCharge(creditCardCharge);
        }

        public void UpdateCCCharge(int chargeId, decimal amount)
        {
            var model = new CreditCardCharge
            {
                Amount = amount
            };

            _repository.UpdateCharge(chargeId, model);
        }
    }
}