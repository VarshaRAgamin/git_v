using Microsoft.AspNetCore.Mvc;
using Vroom_Project.AppDbContext;
using Vroom_Project.Entities;
using Vroom_Project.ViewModels;
using Vroom_Project.Views.Test;

namespace Vroom_Project.Controllers.Banking
{
    public class DemandController : Controller
    {
        private readonly VroomDbContext _context;

        public DemandController(VroomDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var result = ListOfDemandedAccounts();
            return View(result);
        }
        [HttpGet]
        public IEnumerable<ApprovedViewModel> ListOfDemandedAccounts()
        {
            var accounts = _context.UsersAccount.Where(x => x.IsDemandPayment == true).AsQueryable().Select(x => new ApprovedViewModel
            {
                Id = x.Id,
                Acno = x.AccountNumber,
                Phone = x.Phone,
                AcType = x.AccountType,
                Amount = x.Amount,
                AprvAmt = x.PaymentApprovals.Any() ? x.PaymentApprovals.FirstOrDefault().ApprovedAmount : 0,
                OpenDate = x.CreatedDate.ToString("dd, MM, yyyy"),
                FullName=x.FirstName+" "+x.LastName
            });
            return accounts.ToList();
        }
        
        public IActionResult AcceptDemand(int id)
        {
            var accountRepo = _context.UsersAccount.SingleOrDefault(x => x.Id == id);
            if (accountRepo.IsDemandPayment == false)
            {
                this.AddToastrMessage("Error", "Demand isnt approved", "error");
            }
            if (accountRepo.MaturityDate > DateTime.UtcNow)
            {
                this.AddToastrMessage("Error", "Account isnt matured yet", "error");
            }


            var totalPrincipalAmount = accountRepo.Amount;
            decimal annualInterestRate = 4;
            int totalDaysInYear = 365;
            int extraDays = (DateTime.UtcNow - accountRepo.MaturityDate).Days;

            decimal interest = CalculateYearlyInterest(totalPrincipalAmount, annualInterestRate, extraDays, totalDaysInYear);
            Console.WriteLine("Yearly interest with extra days: " + interest);

            var paymentApproval = new PaymentApproval
            {
                AccountId = id,
                ApprovalDate = DateTime.UtcNow,
                TenantId = 1,
                IsPaid = false,
                PaidBy = "Admin",
                ApprovedAmount = interest + totalPrincipalAmount,
                ApprovedByName = "Admin"
            };
            _context.PaymentApprovals.Add(paymentApproval);
            _context.SaveChanges();
            this.AddToastrMessage("success", "Demand accepted successfully.", "error");
            return RedirectToAction("Index");
        }
        public static decimal CalculateYearlyInterest(decimal principal, decimal rate, int days, int totalDaysInYear)
        {
            decimal time = (decimal)days / totalDaysInYear;
            decimal interest = (principal * rate * time) / 100;
            return interest;
        }
        public IActionResult MakePayment(int id)
        {
            var paymentApproval = _context.PaymentApprovals.SingleOrDefault(x => x.AccountId == id);
            if (paymentApproval == null)
            {
                this.AddToastrMessage("Error", "Payment Approval not found", "error");
            }

            if (paymentApproval.IsPaid)
            {
                this.AddToastrMessage("Error", "Payment has already made", "error");
            }

            paymentApproval.IsPaid = true;
            paymentApproval.PaidAmount = paymentApproval.ApprovedAmount;
            paymentApproval.PaidBy = paymentApproval.ApprovedByName;
            paymentApproval.PaidDate = DateTime.UtcNow;
            _context.PaymentApprovals.Update(paymentApproval);
            _context.SaveChanges();
            this.AddToastrMessage("success", "Payment made successfully", "Account");
            return RedirectToAction("Index");
        }

    }
}
