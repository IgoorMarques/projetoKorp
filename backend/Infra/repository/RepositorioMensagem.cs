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
    public class RepositorioMensagem : RepositorioGeneric<Mensagem>, InterfaceMensagem
    {
        public RepositorioMensagem(ContextBase context) : base(context)
        {
        }
    }
}
