using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Manager.Events
{
    public class MovementValidationArgs : EventArgs
    {
        public MovementValidationArgs(MovimentosEnum movimentoValidado)
        {
            MovimentoValidado = movimentoValidado;
        }

        public MovimentosEnum MovimentoValidado
        {
            get;
            set;
        }
    }
}
