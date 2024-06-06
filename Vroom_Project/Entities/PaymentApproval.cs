using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.Entities
{
    public class PaymentApproval :IBaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime ApprovalDate { get; set; }
        public bool IsPaid { get; set; }
        public  decimal PaidAmount { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaidBy { get; set; }
        public int TenantId { get; set; }

        public virtual UserAccount Account { get; set; }
        
    }
}
