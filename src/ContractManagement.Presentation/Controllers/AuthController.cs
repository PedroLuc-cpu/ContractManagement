//using ContractManagement.Infrastructure.Identity;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace ContractManagement.Presentation.Controllers
//{
//    [ApiController]
//    [Route("autenticar")]
//    public sealed class AuthController(SignInManager<ApplicationUser> signInManager,
//                          UserManager<ApplicationUser> userManager) : MainController
//    {
//        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
//        private readonly UserManager<ApplicationUser> _userManager = userManager;

//        [AllowAnonymous]
//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] Login request)
//        {
//            var user = await _userManager.FindByEmailAsync(request.Email);
//            if (user == null)
//                return Unauthorized("Usuário ou senha inválidos.");
//            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
//            if (!result.Succeeded)
//                return Unauthorized("Usuário ou senha inválidos.");
//            // Aqui você pode gerar um token JWT ou configurar a autenticação conforme necessário
//            return Ok("Login bem-sucedido.");
//        }
//        [AllowAnonymous]
//        [HttpPost("logout")]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return Ok("Logout bem-sucedido.");
//        }

//        [AllowAnonymous]
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] Login request)
//        {
//            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
//            var result = await _userManager.CreateAsync(user, request.Password);
//            if (!result.Succeeded)
//                return BadRequest(result.Errors);
//            return Ok("Registro bem-sucedido.");
//        }

//    }
//    public sealed record Login(string Email, string Password);
//}
