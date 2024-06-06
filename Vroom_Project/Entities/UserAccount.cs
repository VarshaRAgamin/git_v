using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.Entities
{
    public class UserAccount: IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string Contenttype { get; set; }
        public bool IsDemandPayment { get; set; }
        public DateTime DemandDate { get; set; }

        public virtual IList<PaymentApproval> PaymentApprovals { get; set; }
      
    }
}
