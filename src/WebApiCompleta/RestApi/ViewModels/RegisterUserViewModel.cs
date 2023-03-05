﻿using System.ComponentModel.DataAnnotations;

namespace RestApi.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage = "O campo {0 está em formato invalido}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters",MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
    public class UserTokenViewModel
    {

        public string Id { get; set; }

        public string Email { get; set; }


        public IEnumerable<ClaimViewModel> ClaimViewModels { get; set; }
    }
    public class LoginResponseViewModel
    {
        
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }
        
        public UserTokenViewModel UserTokenViewModel { get; set; }
    }
    
    public class ClaimViewModel
    {     
        public string Value { get; set; }
       
        public string Type { get; set; }
       
        public string ConfirmPassword { get; set; }
    }
}
