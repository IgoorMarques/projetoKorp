using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Conversa
    {

        [Key]
        public int ConversaId { get; set; }
        public DateTime DataHoraCriacao { get; set; }

        public ICollection<UsuariosConversa> ParticipantesConversa { get; } = new List<UsuariosConversa>();
        public ICollection<Mensagem> Mensagens { get; } = new List<Mensagem>();
    }
}
