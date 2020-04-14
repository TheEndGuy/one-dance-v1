using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Game.Main;
using OneDance.Core.Game.Manager.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Treinamento
{
    public class TreinamentoGame : AbstractGame
    {
        public Action<ValidacaoEnum> PositionIdentified;

        public TreinamentoGame(List<PositionTemplate> posicoesValidacao)
            : base(posicoesValidacao)
        {
        }

        public TreinamentoEnum TipoTreinamento
        {
            get;
            set;
        }
        
        public bool Enabled
        {
            get;
            set;
        }
        
        private int m_index;

        public override int CurrentIndex
        {
            get { return TipoTreinamento == TreinamentoEnum.REFAZER ? m_index - 1 : m_index; }
            set { m_index = value; }
        }

        public override bool CanNext()
        {
            return Validacoes.Count > CurrentIndex + 1;
        }
        
        public override void Finish()
        {
            base.Finish();
            Enabled = false;

            if (TipoTreinamento == TreinamentoEnum.PROXIMA)
                m_index++;
        }
        
        public MovimentosEnum GetPosition()
        {
            if (TipoTreinamento == TreinamentoEnum.PROXIMA && Validacoes.All(x => x.IsChecked))
                return MovimentosEnum.DISABLE;

            if (TipoTreinamento == TreinamentoEnum.PROXIMA)
                return CanNext() ? Validacoes[CurrentIndex + 1].Movimento : MovimentosEnum.UNKNOWN;

            else
                return Validacoes[CurrentIndex].Movimento;
        }
        
        public override void OnPositionIdentified(object sender, EventArgs validationEvent)
        {
            if (!Enabled)
                return;

            if (State == Enums.Gameplay.GameStateEnum.Paused)
                return;

            if (validationEvent == null || Validacoes[CurrentIndex].Movimento != (validationEvent as MovementValidationArgs).MovimentoValidado)
                return;

            var movimentoAtual = Validacoes.Where(x => x.Movimento == Validacoes[CurrentIndex].Movimento)?.FirstOrDefault();

            if (movimentoAtual == null)
                return;
            
            if (movimentoAtual.IsChecked)
                return;

            movimentoAtual.ValidarMovimento();

            PositionIdentified?.Invoke(ValidacaoEnum.VALIDO);
        }
    }
}
