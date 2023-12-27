namespace ETicaretUygulamasi.ViewModels
{
    public class PaymentWithDrawMoneyModel
    {
        public string CardHolder { get; set; }
        public string CardNo { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Cvc { get; set; }
    }
}
