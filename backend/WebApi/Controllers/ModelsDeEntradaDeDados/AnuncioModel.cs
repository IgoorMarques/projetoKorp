using System.ComponentModel.DataAnnotations;

namespace webApi.Controllers.ModelsDeEntradaDeDados
{
    public class AnuncioModel
    {
        [Required]
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        [Required]
        public string NomeAnimal { get; set; } = null!;
        [Required]
        public string Tamanho { get; set; } = null!;
        [Required]
        public int Idade { get; set; }
        [Required]
        public string Especie { get; set; } = null!;
        [Required]
        public string AnuncianteId { get; set; }

        public List<IFormFile> Imagens { get; set; } = new List<IFormFile>();
    }
}
