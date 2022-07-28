using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnicalityTestWebApp.Models;

namespace TechnicalityTestWebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PaymentsController(AppDbContext context, HttpClient httpClient, IConfiguration config)
        {
            _context = context;
            _httpClient = httpClient;
            _config = config;
        }

        // GET: Payments
        public async Task<IActionResult> Index(int id)
        {
            // id is Customer Id
            ViewData["CustomerId"] = id;
            var appDbContext = _context.Payments.Include(p => p.Customer).Where(p => p.CustomerId == id);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create(int id)
        {
            // id is Customer Id

            ViewData["CustomerId"] = id;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Amount")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                // Call Credit Card API
                var vm = new CCChargeViewModel
                {
                    CustomerId = payment.CustomerId,
                    Amount = payment.Amount
                };

                var chargeJson = JsonSerializer.Serialize(vm);
                var requestContent = new StringContent(chargeJson, Encoding.UTF8, "application/json");
                var url = _config["ApiUrl"] + "/CCCharge";
                var response = await _httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    string contentString = await response.Content.ReadAsStringAsync();
                    payment.CreditCardChargeId = JsonSerializer.Deserialize<int>(contentString);
                }

                payment.PaymentDateTime = DateTime.UtcNow;
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = payment.CustomerId });
            }
            ViewData["CustomerId"] = payment.CustomerId;
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = payment.CustomerId;
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,CustomerId,PaymentDateTime,Amount,CreditCardChargeId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (payment.CreditCardChargeId.HasValue)
                    {
                        // Call Credit Card API
                        CCChargeViewModel vm = new CCChargeViewModel
                        {
                            CustomerId = payment.CustomerId,
                            Amount = payment.Amount,
                            ChargeId = payment.CreditCardChargeId.Value
                        };

                        var chargeJson = JsonSerializer.Serialize(vm);
                        var requestContent = new StringContent(chargeJson, Encoding.UTF8, "application/json");
                        var url = _config["ApiUrl"] + "/CCCharge";
                        var response = await _httpClient.PutAsync(url, requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            _context.Update(payment);
                            await _context.SaveChangesAsync();
                        } 
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = payment.CustomerId });
            }
            ViewData["CustomerId"] = payment.CustomerId;
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = payment.CustomerId });
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
