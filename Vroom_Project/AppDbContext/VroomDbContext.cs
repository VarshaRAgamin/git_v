using Microsoft.EntityFrameworkCore;
using Vroom_Project.Entities;
using Vroom_Project.Models;
using Vroom_Project.ViewModels;

namespace Vroom_Project.AppDbContext
{
    public class VroomDbContext:DbContext
    {
        public VroomDbContext(DbContextOptions<VroomDbContext> options): base(options)
        {
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
      
        public DbSet<Sample> Samples { get; set; }
    
        // public DbSet <Account> Accounts { get; set; }
        public DbSet<UserAccount> UsersAccount { get; set; }
        public DbSet<Renewal> Renewals { get; set; }
        public DbSet<Commission> Commissions { get; set; }
        public DbSet<PaymentApproval> PaymentApprovals { get; set; }
        public DbSet<PaidCommission> PaidCommissions { get; set; }
      
       
    }
}
