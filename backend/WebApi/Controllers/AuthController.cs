using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Entities; // Namespace onde a entidade Usuario está localizada
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Cria um token JWT para autenticação do usuário entrar no sistema da clínica petz.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/CreateToken
        ///     {
        ///         "Email": "usuario@exemplo.com",
        ///         "Password": "senha"
        ///     }
        ///     
        /// O token gerado será retornado na resposta.
        /// </remarks>
        /// <param name="loginModel">Dados de login do usuário.</param>
        /// <returns>O token JWT gerado se a autenticação for bem-sucedida.</returns>
        /// <response code="200">Retorna o token JWT se a autenticação for bem-sucedida.</response>
        /// <response code="401">Retorna não autorizado se a autenticação falhar.</response>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Username e senha são obrigatórios.");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Modelo para a solicitação de login
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
