using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.ViewModels
{
    public class ApprovedViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int Amount { get; set; }
        public string Acno { get; set; }
        public decimal AprvAmt {  get; set; } 
        public string AcType { get; set; }
        public string OpenDate { get; set; }
       
    }
}
