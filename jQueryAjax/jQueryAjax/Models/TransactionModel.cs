using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jQueryAjax.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
     
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Account Number")]
        [Column(TypeName = "nvarchar(12)")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Beneficiary Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string BeneficiaryName{ get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Beneficiary Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Swift Code")]
        [Column(TypeName = "nvarchar(10)")]
        public string SwiftCode{ get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Amount")]
        [Column(TypeName = "nvarchar(10)")]
        public int Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        public virtual int BankId { get; set; }
        [ForeignKey("BankId")]
        public virtual BankingModel Bankings { get; set; }
    }
}
