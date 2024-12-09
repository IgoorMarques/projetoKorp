using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Mensagem
    {
        [Key]
        public int MensagemId { get; set; }
        public string? Conteudo { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public bool StatusLeitura { get; set; }
        public int ConversaId { get; set; }
        public string RemetendeId { get; set; }


        [JsonIgnore]
        public Conversa Conversa { get; set; }
        public Usuario Remetente { get; set; }
    }
}
