using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Mensagem
    {
        public int MensagemId { get; set; }
        public string? Conteudo { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public bool StatusLeitura { get; set; }
        public int ConversaId { get; set; }
        public int RemetendeId { get; set; }
        public Conversa Conversa { get; set; }
        public Usuario Remetente { get; set; }
    }
}
