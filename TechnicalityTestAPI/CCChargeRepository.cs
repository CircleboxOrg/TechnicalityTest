using System.Collections.Generic;
using System.Linq;

namespace TechnicalityTestAPI
{
    public class CCChargeRepository : ICCChargeRepository
    {
        private readonly ApiDbContext _context;

        public CCChargeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public List<CreditCardCharge> GetCharges(int customerId)
        {
            return _context.CreditCardCharges.Where(c => c.CustomerId == customerId).ToList();
        }

        public CreditCardCharge Get(int chargeId)
        {
            return _context.CreditCardCharges.Find(chargeId);
        }

        public int CreateCharge(CreditCardCharge model)
        {
            _context.CreditCardCharges.Add(model);
            _context.SaveChanges();
            return model.CreditCardChargeId;
        }

        public void DeleteCharge(int chargeId)
        {
            var ccc = _context.CreditCardCharges.Find(chargeId);
            _context.CreditCardCharges.Remove(ccc);
            _context.SaveChanges();
        }

        public void UpdateCharge(int id, CreditCardCharge model)
        {
            var ccc = _context.CreditCardCharges.Find(id);
            ccc.Amount = model.Amount;
            _context.SaveChanges();
        }
    }
}