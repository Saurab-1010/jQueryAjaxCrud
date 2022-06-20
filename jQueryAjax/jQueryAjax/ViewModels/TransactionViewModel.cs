using Microsoft.AspNetCore.Mvc.Rendering;

namespace jQueryAjax.ViewModels
{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            BankList = new List<SelectListItem>();
        }
        public string AccountNumber { get; set; }

        public int TransactionId { get; set; }

        public string BeneficiaryName { get; set; }

        //public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }
        public int BankId { get; set; }
        public List<SelectListItem> BankList { get; set; }
        public string? BankingName { get; set; }
    }
}
