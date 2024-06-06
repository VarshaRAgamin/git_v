using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.Entities
{
    public class Commission:IBaseEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public int TenantId { get; set; }
        public int AccountId { get; set; }  

        public virtual UserAccount Account {  get; set; } 
        public IList<PaidCommission> PaidCommissions { get; set; }
    }
}
