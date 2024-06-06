using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.ViewModels
{
    public class PaymentModel
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int InitialAmount { get; set; }
        public decimal ApprovedAmount { get; set; }

        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }

        public string PaidBy { get; set; }
        public DateTime PaidDate { get; set; }
       
    }
}
