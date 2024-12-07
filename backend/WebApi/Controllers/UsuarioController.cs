using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuariosController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuario">Objeto que contém as informações do usuário a ser criado.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário")]
        [SwaggerResponse(200, "Usuário criado com sucesso")]
        [SwaggerResponse(400, "Dados do usuário inválidos ou erro na criação")]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados do usuário inválidos");
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = usuario.Nome,
                UserName = usuario.Email,
                Email = usuario.Email
            };

            var result = await _userManager.CreateAsync(novoUsuario, usuario.Senha);
            if (result.Succeeded)
            {
                return Ok("Usuário criado com sucesso");
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os usuários")]
        [SwaggerResponse(200, "Lista de usuários retornada com sucesso")]
        public async Task<IActionResult> ListUsuarios()
        {
            var usuarios = _userManager.Users;
            return Ok(await Task.FromResult(usuarios));
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser buscado.</param>
        /// <returns>O usuário com o ID especificado ou um erro de não encontrado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário pelo ID")]
        [SwaggerResponse(200, "Usuário encontrado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> GetUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza as informações de um usuário existente.
        /// </summary>
        /// <param name="usuarioId">O ID do usuário a ser atualizado.</param>
        /// <param name="usuarioModel">Objeto que contém as novas informações do usuário.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza as informações de um usuário existente")]
        [SwaggerResponse(200, "Usuário atualizado com sucesso")]
        [SwaggerResponse(400, "Dados do usuário inválidos ou erro na atualização")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> UpdateUsuario([FromQuery] string usuarioId, [FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel == null)
            {
                return BadRequest("Dados do usuário inválidos");
            }

            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.UserName = usuarioModel.Email;
            usuario.Email = usuarioModel.Email;

            var result = await _userManager.UpdateAsync(usuario);

            if (result.Succeeded)
            {
                return Ok("Usuário atualizado com sucesso");
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser deletado.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário pelo ID")]
        [SwaggerResponse(200, "Usuário deletado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        [SwaggerResponse(400, "Erro na exclusão do usuário")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);

            if (result.Succeeded)
            {
                return Ok("Usuário deletado com sucesso");
            }

            return BadRequest(result.Errors);
        }
    }

    public class UsuarioModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
