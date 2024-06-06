using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.ViewModels
{
    public class DashboardInfoModel
    {
        public int TotalAccounts { get; set; }
        public int NewAccounts { get; set; }
        public int PaidAccounts { get; set; }
        public int UnpaidAccounts { get; set; }
        public int DemandedAccounts { get; set; }
    }
}
