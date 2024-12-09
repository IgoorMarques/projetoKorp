using Domain.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Controllers.ModelsDeEntradaDeDados;

namespace webApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ConversaController : ControllerBase
    {
        private readonly InterfaceConversa _interfaceConversa;
       
        private readonly InterfaceMensagem _interfaceMensagem;
        private readonly UserManager<Usuario> _userManager;


        public ConversaController(InterfaceConversa interfaceConversa, InterfaceMensagem interfaceMensagem, UserManager<Usuario> userManager)
        {
            _interfaceConversa = interfaceConversa;
            _interfaceMensagem = interfaceMensagem;
            _userManager = userManager;
        }


        /// <summary>
        /// Cria uma nova conversa.
        /// </summary>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova conversa", Description = "Cria uma nova conversa com as informações fornecidas.")]
        [SwaggerResponse(200, "Conversa criada com sucesso", typeof(Conversa))]
        [SwaggerResponse(400, "Dados da conversa inválidos ou erro na criação")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<IActionResult> CreatConversa([FromBody] ConversaModel novaConversa)
        {
            try
            {
                Conversa result = await _interfaceConversa.Add(new Conversa
                {
                    Participante1Id = novaConversa.Participante1Id,
                    Participante2Id = novaConversa.Participante2Id
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Retorna uma resposta genérica de erro interno do servidor
                return StatusCode(500, $"Erro interno do servidor. Por favor, tente novamente mais tarde. {ex}");
            }
        }


        /// <summary>
        /// Lista tadas as conversas cadastradas.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todas as conversas")]
        [SwaggerResponse(200, "Lista de conversas retornada com sucesso")]
        public async Task<IActionResult> ListConversas()
        {
            var conversas = await _interfaceConversa.List();
            return Ok(conversas);
        }

        /// <summary>
        /// Obtém uma conversa pelo ID.
        /// </summary>
        /// <param name="id">O ID da conversa a ser buscada.</param>
        /// <returns>A conversa com o ID especificado ou um erro de não encontrado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Obtém uma conversa pelo ID")]
        [SwaggerResponse(200, "Conversa encontrada com sucesso")]
        [SwaggerResponse(404, "Conversa não encontrada")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            Conversa conversaExistente = await _interfaceConversa.GetEntityByID(id);

            if(conversaExistente == null)
            {
                return NotFound();
            }
            return Ok(conversaExistente);
        }

        /// <summary>
        /// Atualiza as informações de uma conversa existente.
        /// </summary>
        /// <param name="conversaId">O ID da conversa a ser atualizada.</param>
        /// <param name="conversaModel">Objeto que contém as novas informações da conversa.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza as informações de uma conversa existente")]
        [SwaggerResponse(200, "Conversa atualizada com sucesso")]
        [SwaggerResponse(400, "Dados da conversa inválidos ou erro na atualização")]
        [SwaggerResponse(404, "Conversa não encontrada")]
        public async Task<IActionResult> UpdateUsuario([FromQuery] int conversaId, [FromBody] Conversa conversaModel)
        {
            if (conversaModel == null)
            {
                return BadRequest("Dados da conversa inválidos");
            }

            Conversa conversaExistente = await _interfaceConversa.GetEntityByID(conversaId);
            if (conversaExistente == null)
            {
                return NotFound();
            }
            try
            {
                conversaModel.ConversaId = conversaExistente.ConversaId;
                Conversa conversaAtualizada = await _interfaceConversa.Update(conversaModel);
                return Ok(conversaAtualizada);

            }catch(Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor. Por favor, tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Deleta uma conversa pelo ID.
        /// </summary>
        /// <param name="id">O ID da conversa a ser deletada.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Deleta uma conversa pelo ID")]
        [SwaggerResponse(200, "Conversa deletada com sucesso")]
        [SwaggerResponse(404, "Conversa não encontrada")]
        [SwaggerResponse(400, "Erro na exclusão da conversa")]
        public async Task<IActionResult> DeleteConversa(int id)
        {
            Conversa conversaExistente = await _interfaceConversa.GetEntityByID(id);
            if (conversaExistente == null)
            {
                return NotFound();
            }
            try
            {

                await _interfaceConversa.Delete(conversaExistente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor. Por favor, tente novamente mais tarde.");
            }
        }


        /// <summary>
        /// Enviar uma nova mensagem
        /// </summary>
        /// <param name="usuarioId">O ID do remetente da mensagem.</param>
        /// <returns>Mensagem enviada com sucesso.</returns>
        [HttpPost]
        [Route("EnviarMensagem")]
        [SwaggerOperation(Summary = "Enviar uma nova mensagem")]
        [SwaggerResponse(200, "Mensagem enviada com sucesso")]
        [SwaggerResponse(404, "Não encontrado para o id informado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<IActionResult> EnviarMensagem([FromBody] MensagemModel novaMensagem)
        {
            Usuario? usuario = await _userManager.FindByIdAsync(novaMensagem.RemetenteId);
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado para o id informado");
            }

            Conversa conversa = await _interfaceConversa.GetEntityByID(novaMensagem.ConversaId);

            if (conversa == null)
            {
                return NotFound("Conversa não encontrada para o id informado");
            }

            try
            {
                Mensagem createdNovaMensagem = await _interfaceMensagem.Add(new Mensagem { 
                    RemetendeId = novaMensagem.RemetenteId,
                    Conteudo = novaMensagem.Mensagem,
                    ConversaId = novaMensagem.ConversaId
                });
                
                return Ok(createdNovaMensagem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor. Por favor, tente novamente mais tarde.");
            }
        }


    }
}
