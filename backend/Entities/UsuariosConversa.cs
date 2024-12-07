using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuariosConversa
    {
        [Key]
        public int UsuariosConversaId { get; set; }
        public int ConversaId { get; set; }
        public string Participante1Id { get; set; }
        public string Participante2Id { get; set; }

        public Conversa Conversa { get; set; } = null!;
        public Usuario Participante1 { get; set; } = null!;
        public Usuario Participante2 { get; set; } = null!;

    }
}
