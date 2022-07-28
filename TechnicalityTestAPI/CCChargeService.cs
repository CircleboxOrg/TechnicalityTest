using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            foreach (var charge in charges)
            {
                var item = new CCChargeViewModel();
                item.ChargeId = charge.CreditCardChargeId;
                item.CustomerId = charge.CustomerId;
                item.Amount = charge.Amount;
            }

            return list;
        }

        public int CreateCCCharge(int customerId, decimal amount)
        {
            var model = new CreditCardCharge();
            model.CustomerId = customerId;
            model.Amount = amount;
            model.ChargeDateTime = DateTime.UtcNow;
            return _repository.CreateCharge(model);
        }

        public void UpdateCCCharge(int chargeId, decimal amount)
        {
            CreditCardCharge model = new CreditCardCharge
            {
                Amount = amount,
                ChargeDateTime = DateTime.UtcNow
            };

            _repository.UpdateCharge(chargeId, model);
        }

    }
}
