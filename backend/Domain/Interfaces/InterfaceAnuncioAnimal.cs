using dominio.Interfaces.Generics;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface InterfaceAnuncioAnimal : InterfaceGeneric<AnuncioAnimal>
    {
        Task<IEnumerable<AnuncioAnimal>> GetAnuncios();
        Task<AnuncioAnimal> GetAnuncioById(int anuncioId);
    }
}
