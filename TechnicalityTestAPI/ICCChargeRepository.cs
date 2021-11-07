using System.Collections.Generic;

namespace TechnicalityTestAPI
{
    public interface ICCChargeRepository
    {
        List<CreditCardCharge> GetCharges(int customerId);

        CreditCardCharge Get(int chargeId);

        int CreateCharge(CreditCardCharge model);

        void UpdateCharge(int id, CreditCardCharge model);

        void DeleteCharge(int chargeId);
    }
}