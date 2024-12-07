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
    public class RepositorioMidia : RepositorioGeneric<Midia>, InterfaceMidia
    {
        public RepositorioMidia(ContextBase context) : base(context)
        {
        }
    }
}
