using Domain.Interfaces;
using Entities;
using Entities.Context;
using Infra.repository.generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.repository
{
    public class RepositorioAdocao : RepositorioGeneric<Adocao>, InterfaceAdocao
    {
        public RepositorioAdocao(ContextBase context) : base(context)
        {
        }
    }
}
