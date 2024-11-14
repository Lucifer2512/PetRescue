namespace tutor_server.WebPage.ViewModels
{
    public class TransactionModel
    {


        public decimal? Amount { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }

        public string? PaymentMethod { get; set; }
        public DateTime? DonationDate { get; set; }

    }
}
