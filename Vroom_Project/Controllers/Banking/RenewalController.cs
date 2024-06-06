using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Vroom_Project.AppDbContext;
using Vroom_Project.ViewModels;

namespace Vroom_Project.Controllers.Banking
{
    public class RenewalController : Controller
    {
        private readonly VroomDbContext _context;

        public RenewalController(VroomDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var result = ListOfRenewal();
            return View(result);
        }
        public IEnumerable<RenewalViewModel> ListOfRenewal()
        {
            var renewalData = _context.Renewals
                .GroupBy(x => x.AccountId)
                .Select(g => new
                {
                    Id = g.Key,
                    Amount = g.Sum(x => x.Amount),
                    RenewalDate = g.Max(x => x.RenewalDate)
                })
                .ToList(); 
            var renewals = renewalData.Select(x => new RenewalViewModel
            {
                Id = x.Id,
                Amount = x.Amount,
                RenewalDate = x.RenewalDate.ToString("dd MM yyyy") 
            });
            return renewals;
        }
    }
}
