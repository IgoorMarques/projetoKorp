using Domain.Interfaces;
using Entities;
using Entities.Context;
using Infra.repository.generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.repository
{
    public class RepositorioAnuncioAnimal : RepositorioGeneric<AnuncioAnimal>, InterfaceAnuncioAnimal
    {
        private readonly ContextBase _context;
        public RepositorioAnuncioAnimal(ContextBase context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnuncioAnimal>> GetAnuncios()
        {
            return await _context.Set<AnuncioAnimal>()
                .Select(anuncio => new AnuncioAnimal
                {
                    // Copia as propriedades da entidade principal
                    AnuncioId = anuncio.AnuncioId,
                    AnuncianteId = anuncio.AnuncianteId,
                    NomeAnimal = anuncio.NomeAnimal,
                    Titulo = anuncio.Titulo,
                    Descricao = anuncio.Descricao,
                    Midias = anuncio.Midias.Take(1).ToList()
                })
                .ToListAsync();
        }

        public async Task<AnuncioAnimal> GetAnuncioById(int anuncioId)
        {
            return await _context.Set<AnuncioAnimal>()
                .Where(a=>a.AnuncioId == anuncioId)
                .Include(a => a.Midias)
                .FirstOrDefaultAsync();
        }
    }
}
