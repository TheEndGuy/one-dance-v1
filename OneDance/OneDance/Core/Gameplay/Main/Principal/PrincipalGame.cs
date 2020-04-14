using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDance.Core.Enums;
using OneDance.Core.Game.Manager.Events;
using OneDance.Core.Utilities;
using OneDance.Core.Database.Templates;

namespace OneDance.Core.Game.Main.Principal
{
    public class PrincipalGame : AbstractGame
    {
        public Action<ValidacaoEnum> PositionIdentified;

        public PrincipalGame(List<PositionTemplate> posicoesValidacao) 
            : base(posicoesValidacao)
        {
            TotalValidacoes = new Dictionary<MovimentosEnum, int>(posicoesValidacao.ToDictionary(x => x.MovementId, x => 0));
        }

        private Dictionary<MovimentosEnum, int> TotalValidacoes
        {
            get;
            set;
        }

        public override int CurrentIndex
        {
            get;
            set;
        }

        private MovimentosEnum m_lastPositon = MovimentosEnum.UNKNOWN;

        public MovimentosEnum NextPositionValidation()
        {
            var positions = TotalValidacoes.Where(x => x.Value < Configuration.GAME_VALIDATIONS)?.Select(entry=> entry.Key).ToList();

            if(positions == null || positions.Count == 0x0)
                return MovimentosEnum.DISABLE;
            
            if(m_lastPositon == MovimentosEnum.UNKNOWN)
            {
                var randomPosition = positions.Shuffle().First();

                CurrentIndex = Validacoes.IndexOf(Validacoes.FirstOrDefault(x => x.Movimento == randomPosition));
                m_lastPositon = randomPosition;
            }
            
            else if(positions.Count == 1 && positions.First() == m_lastPositon)
            {
                CurrentIndex = Validacoes.IndexOf(Validacoes.FirstOrDefault(x => x.Movimento == m_lastPositon));
            }

            else
            {
                var randomPosition = positions.Shuffle().FirstOrDefault(entry => entry != m_lastPositon);

                CurrentIndex = Validacoes.IndexOf(Validacoes.FirstOrDefault(x => x.Movimento == randomPosition));
                m_lastPositon = randomPosition;
            }

            TotalValidacoes[m_lastPositon]++;

            return m_lastPositon;
        }

        public override bool CanNext()
        {
            return true;
        }

        private bool CanCheck(MovimentosEnum movimentoValidado)
        {
            return TotalValidacoes[movimentoValidado] >= Configuration.GAME_VALIDATIONS;
        }

        public override void OnPositionIdentified(object sender, EventArgs validationEvent)
        {
            if (State == Enums.Gameplay.GameStateEnum.Paused)
                return;

            if (validationEvent == null || Validacoes[CurrentIndex].Movimento != (validationEvent as MovementValidationArgs).MovimentoValidado)
                return;

            var movimentoAtual = Validacoes.FirstOrDefault(x => x.Movimento == (validationEvent as MovementValidationArgs).MovimentoValidado);

            if (movimentoAtual == null)
                return;

            if (movimentoAtual.IsChecked)
                return;

            if (CanCheck(movimentoAtual.Movimento))
                movimentoAtual.ValidarMovimento();

            PositionIdentified?.Invoke(ValidacaoEnum.VALIDO);
        }

        public void ResetGame()
        {
            TotalValidacoes.Keys.ToList().ForEach(x => TotalValidacoes[x] = 0);
        }
    }
}
