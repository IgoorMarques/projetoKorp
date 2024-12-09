using System.ComponentModel.DataAnnotations;

namespace webApi.Controllers.ModelsDeEntradaDeDados
{
    public class MensagemModel
    {
        [Required]
        public string RemetenteId { get; set; }
        [Required]
        public string Mensagem { get; set; } = null!;
        [Required]
        public int ConversaId { get; set; }

    }
}
