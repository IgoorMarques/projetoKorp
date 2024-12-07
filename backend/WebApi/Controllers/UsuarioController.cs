using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Entities;

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

        // Endpoint para criar um novo usuário
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados do usuário inválidos");
            }

            Usuario novoUsuario = new Usuario();
            novoUsuario.Nome = usuario.Nome;
            novoUsuario.UserName = usuario.Email;
            novoUsuario.Email = usuario.Email;


            var result = await _userManager.CreateAsync(novoUsuario, usuario.Senha);
            if (result.Succeeded)
            {
                return Ok("Usuário criado com sucesso");
            }

            return BadRequest(result.Errors);
        }

        // Endpoint para listar todos os usuários
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListUsuarios()
        {
            var usuarios = _userManager.Users;
            return Ok(await Task.FromResult(usuarios));
        }

        // Endpoint para obter um usuário por ID
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUsuario(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // Endpoint para atualizar um usuário existente
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUsuario([FromQuery] string usuarioId ,[FromBody] UsuarioModel usuarioModel)
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

            usuario.UserName = usuario.UserName;
            usuario.Email = usuario.Email;
            usuario.ImagemPerfilUrl = usuario.ImagemPerfilUrl;

            var result = await _userManager.UpdateAsync((Usuario)usuario);

            if (result.Succeeded)
            {
                return Ok("Usuário atualizado com sucesso");
            }

            return BadRequest(result.Errors);
        }

        // Endpoint para deletar um usuário
        [HttpDelete]
        [Route("{id}")]
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
}

public class UsuarioModel
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
