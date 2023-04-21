using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity
{
    public class TwoStepModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }
        public bool RememberMe { get; set; }
    }
}
