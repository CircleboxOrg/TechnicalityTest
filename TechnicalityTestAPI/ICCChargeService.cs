using System.Collections.Generic;

namespace TechnicalityTestAPI
{
    public interface ICCChargeService
    {
        List<CCChargeViewModel> GetCharges(int customerId);

        int CreateCCCharge(CCChargeViewModel model);

        void UpdateCCCharge(int chargeId, decimal amount);
    }
}