using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vroom_Project.ViewModels
{
    public class AccountModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Please enter valid email id")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\+?\d{1,3})?[-.\s]?(\d{10})$", ErrorMessage ="Please enter a valid mobile number")]
        public string Phone { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Range(1, 100000)]
        public int Amount { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
        public string? Contenttype { get; set; }
    }
}
