using Microsoft.AspNetCore.Mvc;
using Vroom_Project.AppDbContext;
using Vroom_Project.Entities;
using Vroom_Project.ViewModels;
using Vroom_Project.Views.Test;

namespace Vroom_Project.Controllers.Banking
{
    public class PaymentController : Controller
    {
        private readonly VroomDbContext _context;

        public PaymentController(VroomDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var result = ListOfPayments();
            return View(result);

        }
        public IEnumerable<PaymentModel> ListOfPayments()
        {

            var paidPayment = _context.PaymentApprovals.Where(x => x.IsPaid == true).AsQueryable().Select(x => new PaymentModel
            {
                AccountId = x.Id,
                AccountNumber = x.Account.AccountNumber,
                FullName = x.Account.FirstName + " " + x.Account.LastName,
                Email = x.Account.Email,
                Phone = x.Account.Phone,
                AccountType = x.Account.AccountType,
                InitialAmount = x.Account.Amount,
                PaidBy = x.PaidBy,
                PaidDate = x.PaidDate,
                ApprovedAmount = x.ApprovedAmount,
            });
            return paidPayment.ToList();
        }
        public IActionResult List()
        {
            var result = ListOfUnPaidPayments();
            return View(result);
        }
        public IEnumerable<PaymentModel> ListOfUnPaidPayments()
        {

            var paidPayment = _context.PaymentApprovals.Where(x => x.IsPaid == false).AsQueryable().Select(x => new PaymentModel
            {
                AccountId = x.Id,
                AccountNumber = x.Account.AccountNumber,
                FullName = x.Account.FirstName + " " + x.Account.LastName,
                Email = x.Account.Email,
                Phone = x.Account.Phone,
                AccountType = x.Account.AccountType,
                InitialAmount = x.Account.Amount,
                PaidBy = x.PaidBy,
                PaidDate = x.PaidDate,
                ApprovedAmount = x.ApprovedAmount,
            });
            return paidPayment.ToList();

        }
        [HttpGet]
        public PaymentDetailModel PaymentDetailById(int accountId)
         {
            var repoData = _context.PaymentApprovals.FirstOrDefault(x => x.AccountId == accountId);

                PaymentDetailModel viewModel = new PaymentDetailModel();
                viewModel.Id = accountId;
                viewModel.PaidDate = repoData.PaidDate;
                viewModel.AprvAmt = repoData.ApprovedAmount;
                viewModel.PaidBy = repoData.PaidBy;
                return viewModel;
        }
         
    }
}
