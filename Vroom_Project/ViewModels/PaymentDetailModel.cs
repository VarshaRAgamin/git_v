namespace Vroom_Project.ViewModels
{
    public class PaymentDetailModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public decimal AprvAmt { get; set; }
        public string PaidBy { get; set; }
        public DateTime PaidDate { get; set; }
    }
}
