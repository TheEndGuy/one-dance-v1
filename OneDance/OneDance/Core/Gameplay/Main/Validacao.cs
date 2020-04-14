using OneDance.Core.Database;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Game.Movimentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Treinamento
{
    public class Validacao
    {
        public Validacao(PositionTemplate positionTemplate)
        {
            Template = positionTemplate;
            Movimento = Template.MovementId;
        }
        
        public bool IsChecked
        {
            get;
            private set;
        }

        public PositionTemplate Template
        {
            get;
            set;
        }

        public MovimentosEnum Movimento
        {
            get;
            set;
        }

        public bool PoseValida
        {
            get;
            set;
        }

        public Tracker CriarRastreador()
        {
            return new Tracker(Template);
        }

        public void IniciarValidacao()
        {
            PoseValida = false;
            IsChecked = false;
        }

        public void FinalizarValidacao()
        {
            IsChecked = true;
            PoseValida = false;
        }

        public void ValidarMovimento()
        {
            PoseValida = true;
            IsChecked = true;
        }
    }
}
