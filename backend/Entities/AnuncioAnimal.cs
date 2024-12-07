using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AnuncioAnimal
    {
        [Key]
        public int AnuncioId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string NomeAnimal { get; set; } = null!;
        public string Tamanho { get; set; } = null!;
        public int Idade { get; set; }
        public string Especie { get; set; } = null!;
        public bool Adotado { get; set; } = false;
        public string AnuncianteId { get; set; }
        public Usuario Anunciante { get; set; } = null!;
        public ICollection<Midia> Midias { get; set; } = new List<Midia>();
    }
}
