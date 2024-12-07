using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuariosConversa
    {
        public int UsuariosConversaId { get; set; }
        public int ConversaId { get; set; }
        public int Participante1Id { get; set; }
        public int Participante2Id { get; set; }

        public Conversa Conversa { get; set; } = null!;
        public Usuario Participante1 { get; set; } = null!;
        public Usuario Participante2 { get; set; } = null!;

    }
}
