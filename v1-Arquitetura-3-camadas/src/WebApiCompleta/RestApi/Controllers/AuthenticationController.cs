using Dev.Bussines.Interfaces;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestApi.Extensions;
using RestApi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}")]
    public class AuthenticationController : MainController
    {
        /// <summary>
        /// ////////////
        /// </summary>
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSenttings;
        public AuthenticationController(INotificador notificador, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
            ,IOptions<AppSettings> appSenttings, IUser user,ILogger<AuthenticationController> logger
            ) : base(user,notificador,logger)
        {
            
            _userManager = userManager;
            _signInManager = signInManager;
            _appSenttings = appSenttings.Value;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return CustomReponse(ModelState);

            var createUser = new IdentityUser
            {
                UserName = userViewModel.Email,
                Email = userViewModel.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(createUser, userViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(createUser, false);
                return CustomReponse(await GerarJwt(userViewModel.Email));
            }
            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }          
            return CustomReponse(userViewModel);
        }

        [HttpPost("logar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomReponse();

            
            var resultLoginUser = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (resultLoginUser.Succeeded)
            {
                return CustomReponse(await GerarJwt(loginUser.Email));
            }
            else if (resultLoginUser.IsLockedOut)
            {
                NotificarErro("Usuario bloqueado muitas tentativas foram tentadas");
                return CustomReponse(loginUser);
            }

            NotificarErro("Usuario ou senha incorretos");
            return CustomReponse(loginUser);
        }
        [NonAction]
        private  async Task<LoginResponseViewModel> GerarJwt(string buscarEmail)
        {
            var user = await _userManager.FindByEmailAsync(buscarEmail);
            var receberClaims = await CriarAsClaims(user);
            var identityClaims = new ClaimsIdentity(); ;
            identityClaims.AddClaims(receberClaims);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSenttings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSenttings.Emissor,
                Audience = _appSenttings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSenttings.ExpitacaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            var encodedToken = tokenHandler.WriteToken(token);
            var response = await ReturnLoginResponseViewModel(user,encodedToken,receberClaims);
            
            return response;
        }
        [NonAction]
        private async Task<LoginResponseViewModel> ReturnLoginResponseViewModel(IdentityUser receiveUser,string receiveEncodToken,IList<Claim> receiveClaims)
        {
            
            var response = new LoginResponseViewModel
            {
                AccessToken = receiveEncodToken,
                ExpiresIn = TimeSpan.FromHours(_appSenttings.ExpitacaoHoras).TotalSeconds,               
                UserTokenViewModel = new UserTokenViewModel
                {
                    Id = receiveUser.Id,
                    Email = receiveUser.Email,
                    ClaimViewModels = receiveClaims.Select(x => new ClaimViewModel { Type = x.Type, Value = x.Value })
                }
            };
            return response;
        }
        
        [NonAction]
        private async Task<IList<Claim>> CriarAsClaims(IdentityUser userIdentity)
        {            
            var claims = await _userManager.GetClaimsAsync(userIdentity);
            var userRoles = await _userManager.GetRolesAsync(userIdentity);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEporDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEporDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (string userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }
            return claims;
        }
        private static long ToUnixEporDate(DateTime dateTime)
            => (long)Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
