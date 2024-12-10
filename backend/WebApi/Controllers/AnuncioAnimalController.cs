using Domain.Interfaces;
using Domain.Servicos;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Controllers.ModelsDeEntradaDeDados;

namespace webApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioAnimalController : ControllerBase
    {
        private readonly InterfaceAnuncioAnimal _interfaceAnuncioAnimal;
        private readonly UserManager<Usuario> _userManager;
        private readonly S3Service _s3Service;
        private readonly InterfaceMidia _interfaceMidia;
        public AnuncioAnimalController(InterfaceAnuncioAnimal interfaceAnuncioAnimal, UserManager<Usuario> userManager, S3Service s3Service, InterfaceMidia interfaceMidia)
        {
            _interfaceAnuncioAnimal = interfaceAnuncioAnimal;
            _userManager = userManager;
            _s3Service = s3Service;
            _interfaceMidia = interfaceMidia;

        }

        /// <summary>
        /// Cria um novo anúncio.
        /// </summary>
        /// <param name="anuncio">Objeto que contém as informações do anúncio a ser criado.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo anúncio")]
        [SwaggerResponse(201, "Anúncio criado com sucesso")]
        [SwaggerResponse(400, "Dados do anúncio inválidos ou erro na criação")]
        public async Task<IActionResult> CreateAnuncio([FromForm] AnuncioModel anuncio)
        {
            var usuario = await _userManager.FindByIdAsync(anuncio.AnuncianteId);
            if (usuario == null)
            {
                return BadRequest(new { message = "Usuario não encontrdo para o id informado" });
            }

            try
            {
                AnuncioAnimal novoAnuncio = new AnuncioAnimal
                {
                    Titulo = anuncio.Titulo,
                    Descricao = anuncio.Descricao,
                    NomeAnimal = anuncio.NomeAnimal,
                    Especie = anuncio.Especie,
                    Tamanho = anuncio.Tamanho,
                    Idade = anuncio.Idade,
                    AnuncianteId = anuncio.AnuncianteId,
                    Midias = new List<Midia>()
                };

                var result = await _interfaceAnuncioAnimal.Add(novoAnuncio);

                var midias = new List<Midia>();
                if (anuncio.Imagens != null && anuncio.Imagens.Count > 0)
                {
                    for (int i = 0; i < anuncio.Imagens.Count; i++)
                    {
                        var imagem = anuncio.Imagens[i];
                        if (imagem.Length > 0)
                        {
                            using (var stream = imagem.OpenReadStream())
                            {
                                var keyName = $"anuncios/{result.AnuncioId}/images/{imagem.FileName}";
                                var imageUrl = await _s3Service.UploadImageAsync(stream, keyName, imagem.ContentType);
                                if (imageUrl != null)
                                {
                                    Midia img = new Midia
                                    {
                                        UrlMidia = imageUrl,
                                        AnuncioAnimalId = result.AnuncioId,
                                        Ordem = i + 1,
                                        Tipo = "img"
                                    };
                                    await _interfaceMidia.Add(img);
                                    midias.Add(img);
                                }
                            }
                        }
                    }
                }
                result.Midias = midias;
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log da exceção pode ser adicionado aqui
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Lista todos os anuncios cadastrados.
        /// </summary>
        /// <returns>Uma lista de anuncios.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Lista todos os anuncios cadastrados")]
        [SwaggerResponse(200, "Lista de anuncios retornada com sucesso")]
        public async Task<IActionResult> ListAnuncios()
        {
            var anuncios = await _interfaceAnuncioAnimal.GetAnuncios();
            return Ok(anuncios);
        }

        /// <summary>
        /// Obtém um anuncio pelo ID.
        /// </summary>
        /// <param name="id">O ID do anuncio a ser buscado.</param>
        /// <returns>O anuncio com o ID especificado ou um erro de não encontrado.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário pelo ID")]
        [SwaggerResponse(200, "Usuário encontrado com sucesso")]
        [SwaggerResponse(404, "Usuário não encontrado")]
        public async Task<IActionResult> GetAnuncio(int id)
        {
            var anuncio = await _interfaceAnuncioAnimal.GetAnuncioById(id);
            if (anuncio == null)
            {
                return NotFound();
            }

            return Ok(anuncio);
        }

        /// <summary>
        /// Atualiza as informações de um anuncio existente.
        /// </summary>
        /// <param name="anuncioId">O ID do anuncio a ser atualizado.</param>
        /// <param name="anuncio">Objeto que contém as novas informações do anuncio.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Atualiza as informações de um anuncio existente")]
        [SwaggerResponse(200, "Anuncio atualizado com sucesso")]
        [SwaggerResponse(400, "Dados do anuncio inválidos ou erro na atualização")]
        [SwaggerResponse(404, "Anuncio não encontrado")]
        public async Task<IActionResult> UpdateAnuncio([FromQuery] int anuncioId, [FromBody] AnuncioModel anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest(new { message = "Dados do anuncio inválidos" });
            }

            AnuncioAnimal anuncioEncontrado = await _interfaceAnuncioAnimal.GetEntityByID(anuncioId);
            if (anuncio == null)
            {
                return NotFound();
            }

            try
            {
                anuncioEncontrado.Titulo = anuncio.Titulo;
                anuncioEncontrado.Descricao = anuncio.Descricao;
                anuncioEncontrado.NomeAnimal = anuncio.NomeAnimal;
                anuncioEncontrado.Tamanho = anuncio.Tamanho;
                anuncioEncontrado.Idade = anuncio.Idade;
                anuncioEncontrado.Especie = anuncio.Especie;
                var anuncioAtualizado = await _interfaceAnuncioAnimal.Update(anuncioEncontrado);
                return Ok(anuncio);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deleta um anuncio pelo ID.
        /// </summary>
        /// <param name="id">O ID do anuncio a ser deletado.</param>
        /// <returns>Um status de sucesso ou falha com as mensagens apropriadas.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Deleta um anuncio pelo ID")]
        [SwaggerResponse(200, "Anuncio deletado com sucesso")]
        [SwaggerResponse(404, "Anuncio não encontrado")]
        [SwaggerResponse(400, "Erro na exclusão do anuncio")]
        public async Task<IActionResult> DeleteAnuncio(int id)
        {
            var anuncio = await _interfaceAnuncioAnimal.GetEntityByID(id);
            if (anuncio == null)
            {
                return NotFound();
            }

            await _interfaceAnuncioAnimal.Delete(anuncio);

            return NoContent();

            
        }

    }
}
