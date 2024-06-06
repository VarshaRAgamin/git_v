using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.ViewModels
{
    public class NewAccountModel
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Amount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string TenantId { get; set; }
    }
}
