using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Entities; // Namespace onde a entidade Usuario está localizada
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Controller responsável pela autenticação e geração de token JWT.")]
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
        /// Realiza o login de um usuário e retorna um token JWT.
        /// </summary>
        /// <param name="model">Modelo de solicitação de login contendo o nome de usuário e a senha.</param>
        /// <returns>Um token JWT caso as credenciais sejam válidas.</returns>
        /// <response code="200">Retorna um token JWT.</response>
        /// <response code="400">Se o nome de usuário ou a senha estiverem ausentes.</response>
        /// <response code="401">Se as credenciais forem inválidas.</response>
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Login de usuário", Description = "Realiza a autenticação do usuário e retorna um token JWT.")]
        [SwaggerResponse(200, "Token JWT gerado com sucesso", typeof(object))]
        [SwaggerResponse(400, "Requisição inválida - nome de usuário ou senha ausentes")]
        [SwaggerResponse(401, "Credenciais inválidas")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Username e senha são obrigatórios.");
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                return Unauthorized(new { message = "Email ou senha incorretos, verifique suas credenciais" });

            }

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        private string GenerateJwtToken(Usuario user)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: user.Id,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Modelo para a solicitação de login
    public class LoginModel
    {
        /// <summary>
        /// Nome de usuário para autenticação.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário para autenticação.
        /// </summary>
        public string Password { get; set; }
    }
}
