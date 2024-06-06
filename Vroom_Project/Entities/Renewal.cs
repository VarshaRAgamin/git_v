using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.Entities
{
    public class Renewal:IBaseEntity
    {
        public int Id { get; set; }
        public DateTime RenewalDate { get; set; }
        public int Amount { get; set; }
        public int AccountId { get; set; }
        public int TenantId { get; set; }

        public virtual UserAccount Account { get; set; }
        
    }
}
