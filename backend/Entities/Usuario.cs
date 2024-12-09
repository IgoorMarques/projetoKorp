using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string? ImagemPerfilUrl { get; set; }

        [JsonIgnore]
        public ICollection<Conversa> ConversasComoParticipante1 { get; set; } = new List<Conversa>();

        [JsonIgnore]
        public ICollection<Conversa> ConversasComoParticipante2 { get; set; } = new List<Conversa>();
        public ICollection<AnuncioAnimal> Anuncios { get; } =  new List<AnuncioAnimal>();
    }
}
                