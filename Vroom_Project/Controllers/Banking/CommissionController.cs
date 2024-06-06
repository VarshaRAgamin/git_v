using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using Vroom_Project.AppDbContext;
using Vroom_Project.Entities;
using Vroom_Project.ViewModels;
using Vroom_Project.Views.Test;

namespace Vroom_Project.Controllers.Banking
{
    public class CommissionController : Controller
    {
        private readonly VroomDbContext _context;

        public CommissionController(VroomDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var result = GetAll();
            return View(result);
        }
        [HttpGet]
        public IEnumerable<CommissionModel> GetAll()
        {
            var result = _context.Commissions.AsQueryable().Select(x => new CommissionModel
            {
                Id = x.Id,
                Acno = x.Account.AccountNumber,
                Amount = x.Amount,
                CommissionDate = x.CreatedDate.ToString("dd MMM, yyyy"),
                FullName = x.Account.FirstName + " " + x.Account.LastName,
                Status=x.PaidCommissions.Any()?"Paid":"Not Paid"
            });
            return result;
        }

        public IActionResult WithdrawCommission(int id)
        {
            var commission = _context.Commissions.SingleOrDefault(x => x.Id == id);
            if (commission == null)
            {
                this.AddToastrMessage("Error", "No commission found for this account", "error");
            }
            else if (commission.Amount <= 0)
            {
                this.AddToastrMessage("Error", "No commission available for withdrawal", "error");
            }
            else
            {
                var existingPaidCommission = _context.PaidCommissions.FirstOrDefault(x => x.CommissionId == id);

                if (existingPaidCommission != null)
                {
                    this.AddToastrMessage("Error", "Commission has already been withdrawn", "error");
                }
                else
                {
                    var newComm = new PaidCommission
                    {
                        Amount = Convert.ToInt32(commission.Amount),
                        CommissionId = commission.Id,
                        PaidDate = DateTime.UtcNow,
                        UserId = 1, // Adjust this to the appropriate user ID
                    };

                    // Add and save the new paid commission entry
                    _context.PaidCommissions.Add(newComm);
                    _context.SaveChanges();
                    this.AddToastrMessage("success", $"Commission of {newComm.Amount} withdrawn successfully for account ID {id}", "success");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PaidCommission ViewDetails(int id)
        { 
            var repoData=_context.PaidCommissions.FirstOrDefault(x=>x.CommissionId==id);
            return repoData;
        }
    }
}

