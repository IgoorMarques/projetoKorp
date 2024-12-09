using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Conversa
    {
        [Key]
        public int ConversaId { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public string Participante1Id { get; set; } = null!;
        public string Participante2Id { get; set; } = null!;
        public Usuario Participante1 { get; set; } = null!;
        public Usuario Participante2 { get; set; } = null!;
        public ICollection<Mensagem> Mensagens { get; set; } = new List<Mensagem>();
    }
}


