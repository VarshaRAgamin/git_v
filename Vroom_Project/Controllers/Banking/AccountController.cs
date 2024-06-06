using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Protocol.Core.Types;
using System.Security.Principal;
using Vroom_Project.AppDbContext;
using Vroom_Project.Entities;
using Vroom_Project.ViewModels;
using Vroom_Project.Views.Test;

namespace Vroom_Project.Controllers.Banking
{
    public class AccountController : Controller
    {
        private readonly VroomDbContext _context;

        public AccountController(VroomDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            GetAll();
            var data = TempData["accounts"];
            if (data != null)
            {
                return View(data);
            }
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // Path to the default image
            string defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\AIBD1006\\Downloads\\catimage.jpg");
            byte[] defaultImageBytes = System.IO.File.ReadAllBytes(defaultImagePath);
            string defaultImageName = "default.jpg";

            var result = _context.UsersAccount.ToList().Select(x => new ListViewAccounts
            {
                FullName = x.FirstName + " " + x.LastName,
                Amount = x.Amount,
                AccountNumber = x.AccountNumber,
                Id = x.Id,
                CreateDate=x.CreatedDate,
                ImageBase64 = x.Image != null
            ? Convert.ToBase64String(x.Image)
            : Convert.ToBase64String(defaultImageBytes)
            }).OrderByDescending(x=>x.CreateDate);

            TempData["accounts"] = result;
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(AccountModel accountModel)
        {
            if(!ModelState.IsValid)
            {
                var errors=GetModelErrors(ModelState);
                this.AddToastrMessage("error", errors.FirstOrDefault(), "Error");
                return View("Create"); // S
            }
            else
            {
                if (accountModel.Image != null && accountModel.Image.Length > 0)
                {
                    byte[] profileImageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        accountModel.Image.CopyTo(memoryStream);
                        profileImageData = memoryStream.ToArray();
                    }
                    var account = new UserAccount
                    {
                        FirstName = accountModel.FirstName,
                        LastName = accountModel.LastName,
                        Email = accountModel.Email,
                        Phone = accountModel.Phone,
                        AccountType = accountModel.AccountType,
                        Amount = accountModel.Amount,
                        CreatedDate = DateTime.UtcNow,
                        MaturityDate = CalculateMaturityDate(accountModel.AccountType),
                        AccountNumber = GenerateAccountNumber("B001"),
                        Image = profileImageData,
                        ImageName = accountModel.Image.FileName,
                        Contenttype = accountModel.Image.ContentType,

                    };

                    _context.UsersAccount.Add(account);
                    _context.SaveChanges();

                    if (account.Id > 0)
                    {
                        decimal commissionAmount = account.Amount * 0.01m;

                        // Store commission in the database
                        var commission = new Commission
                        {
                            CreatedUserId = 1,
                            TenantId = 1,
                            AccountId = account.Id,
                            Amount = commissionAmount,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.Commissions.Add(commission);
                        _context.SaveChanges();
                        this.AddToastrMessage("success", "Account added successfully", "Account");
                    }

                    return RedirectToAction("Index");
                }

                return BadRequest("Image upload failed");
            }
        }
        private DateTime CalculateMaturityDate(string accountType)
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime maturityDate;

            switch (accountType.ToLower())
            {
                case "daily":
                    maturityDate = currentDate.AddDays(30);
                    break;
                case "monthly":
                    maturityDate = currentDate.AddMonths(24);
                    break;
                case "yearly":
                    maturityDate = currentDate.AddMonths(32);
                    break;
                default:
                    throw new ArgumentException("Invalid account type");
            }

            // Log the calculated maturity date for debugging
            Console.WriteLine($"Calculated maturity date for account type '{accountType}': {maturityDate}");

            return maturityDate;
        }

        private string GenerateAccountNumber(string branchCode)
        {
            var lastAccount = _context.UsersAccount.OrderBy(x => x.Id).LastOrDefault();
            int lastAccountNumber = 0;
            if (lastAccount != null)
            {
                lastAccountNumber = int.Parse(lastAccount.AccountNumber.Substring(branchCode.Length));
            }
            string accountNumber = $"{branchCode}{lastAccountNumber + 1:0000}";
            return accountNumber;
        }


        public IActionResult Edit(int id)
        {
            var data = _context.UsersAccount.Find(id);
            var editData = new EditAccountModel();
            editData.Id = data.Id;
            editData.LastName = data.LastName;
            editData.FirstName = data.FirstName;
            editData.Email = data.Email;
            editData.Amount = data.Amount;
            editData.Phone = data.Phone;
            editData.AccountType = data.AccountType;
            return View(editData);
        }
        [HttpPost]
        public IActionResult EditAccount(EditAccountModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    return BadRequest(" Invalid ID");
                }
                var account = _context.UsersAccount.Find(model.Id);
                if (account == null)
                {
                    return BadRequest("account Id not found");
                }
                account.Id = model.Id;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.AccountType = model.AccountType;
                account.Phone = model.Phone;
                account.Amount = model.Amount;

                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.Image.CopyTo(memoryStream);
                        account.Image = memoryStream.ToArray();
                        account.ImageName = model.Image.FileName;
                    }
                }
                _context.UsersAccount.Update(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetImage(int id)
        {
            var account = _context.UsersAccount.Find(id);

            if (account == null || account.Image == null)
            {
                return NotFound();
            }

            return File(account.Image, account.Contenttype, account.ImageName);
        }
        public IActionResult DeleteAccount(int id)
        {
            var account = _context.UsersAccount.Find(id);
            if (account == null)
            {
                this.AddToastrMessage("error", "Record not found", "Error");
            }
            else
            {
                _context.UsersAccount.Remove(account);
                _context.SaveChanges();
                this.AddToastrMessage("success", "Record deleted successfully", "Success");
            }
            return RedirectToAction("Index");
        }

        [HttpPut]
        public IActionResult DemandRequest(int id)
        {
            try
            {
                var account = _context.UsersAccount.SingleOrDefault(x => x.Id == id);
                if (account == null)
                {
                    throw new Exception("Account not found");
                }
                if (account.MaturityDate > DateTime.UtcNow)
                {
                    throw new Exception("Account is not matured yet.");
                }
                if (account.IsDemandPayment)
                {
                    throw new Exception("Payment demand already made.");
                }
                account.IsDemandPayment = true;
                account.DemandDate = DateTime.UtcNow;

                _context.UsersAccount.Update(account);
                _context.SaveChanges();
                this.AddToastrMessage("success", "Demanded successfully", "Success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastrMessage("error", ex.Message, "Error");
                return BadRequest(new { Message = ex.Message });
            }

        }
        [HttpGet]
        public GetAccountViewModel GetAccountById(int id)
        {
            var repoData = _context.UsersAccount.FirstOrDefault(x => x.Id == id);

            GetAccountViewModel viewModel = new GetAccountViewModel();
            viewModel.Id = repoData.Id;
            viewModel.AccountNumber = repoData.AccountNumber;
            viewModel.FullName = $"{repoData.FirstName} {repoData.LastName}";
            viewModel.MaturityDate = repoData.MaturityDate.ToString("dd MMM, yyyy");
            return viewModel;
        }
        [HttpPost]
        public IActionResult DepositRenewal(int id, int amount)
        {
            try
            {
                var account = _context.UsersAccount.SingleOrDefault(x => x.Id == id);
                if (account == null)
                {
                    throw new Exception("Invalid account ID.");
                }

                if (DateTime.UtcNow <= account.MaturityDate)
                {
                    throw new Exception("Account is matured. Renewal deposit not accepted.");
                }

                var renewal = new Renewal
                {
                    RenewalDate = DateTime.UtcNow,
                    AccountId = account.Id,
                    TenantId = 1,
                    Amount = amount,
                };
                account.Amount += amount;
                _context.Renewals.Add(renewal);
                _context.SaveChanges();
                this.AddToastrMessage("success", "Account renewed successfully.", "Success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.AddToastrMessage("error", ex.Message, "Error");
                return BadRequest(new { Message = ex.Message });
            }
        }
        [NonAction]
        public List<string> GetModelErrors(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }
}

