using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AnuncioAnimal
    {
        public int AnuncioId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string NomeAnimal { get; set; } = null!;
        public string Tamanho { get; set; } = null!;
        public int Idade { get; set; }
        public string Especie { get; set; } = null!;
        public bool Adotado { get; set; } = false;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
