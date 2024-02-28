using System.ComponentModel.DataAnnotations;

namespace RestApi.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage = "O campo {0 está em formato invalido}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 6)]
        public string Password { get; set; }        
    }
}
