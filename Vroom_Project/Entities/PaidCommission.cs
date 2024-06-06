using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.Entities
{
    public class PaidCommission:IBaseEntity
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public int UserId { get; set; }
        public int CommissionId { get; set; }

       public virtual Commission Commission { get; set; }
    }
}
