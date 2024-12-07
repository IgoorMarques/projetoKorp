using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string? ImagemPerfilUrl { get; set; }
        public ICollection<UsuariosConversa> ParticipacaoEmConversas { get; } =  new List<UsuariosConversa>();
        public ICollection<Mensagem> MensagensEnviadas { get; } =  new List<Mensagem       >();
        public ICollection<AnuncioAnimal> Anuncios { get; } =  new List<AnuncioAnimal>();
    }
}
                