namespace Vroom_Project.ViewModels
{
    public class ListViewAccounts
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public string AccountNumber { get; set; }
        public int Amount { get; set; }
        public string ImageBase64 { get; set; }

    }
}
