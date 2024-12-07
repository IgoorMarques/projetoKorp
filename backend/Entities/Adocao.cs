using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public  class Adocao
    {
        public int AdocaoId { get; set; }
        public int UsuarioId { get; set; }
        public int AnuncioAnimalId { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public AnuncioAnimal AnuncioAnimal { get; set; } = null!;
    }
}
