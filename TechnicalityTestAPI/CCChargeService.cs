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
                list.Add(new CCChargeViewModel()
                {
                    ChargeId = charge.CreditCardChargeId,
                    CustomerId = charge.CustomerId,
                    Amount = charge.Amount
                });
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
            var model = new CreditCardCharge();
            model.Amount = amount;

            _repository.UpdateCharge(chargeId, model);
        }

    }
}
