using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace jQueryAjax.Models
{
    public class BankingModel
    {
        [Key]
        public int BankId { get; set; }

        [Required(ErrorMessage="This field is required")]
        public string? Name { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Account Number")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("BeneficiaryName")]
        public string? BeneficiaryName{ get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
