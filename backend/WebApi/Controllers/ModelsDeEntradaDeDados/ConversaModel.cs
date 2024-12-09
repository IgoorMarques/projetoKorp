using System.ComponentModel.DataAnnotations;

namespace webApi.Controllers.ModelsDeEntradaDeDados
{
    public class ConversaModel
    {
        [Required]
        public string Participante1Id { get; set; } = null!;
        [Required]
        public string Participante2Id { get; set; } = null!;
    }
}
