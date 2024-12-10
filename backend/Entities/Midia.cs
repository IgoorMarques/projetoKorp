 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Midia
    {
        [Key]
        public int MidiaId { get; set; }
        public string UrlMidia { get; set; }
        public string Tipo { get; set; }
        public int Ordem { get; set; }

        public int AnuncioAnimalId { get; set; }


        [JsonIgnore]
        public AnuncioAnimal AnuncioAnimal { get; set; }

    }
}
