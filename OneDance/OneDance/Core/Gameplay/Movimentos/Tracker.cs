using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Game.Manager;
using OneDance.Core.Game.Manager.Events;
using OneDance.Core.Game.Movimentos.Gestos;
using OneDance.Core.Game.Movimentos.Gestos.MovimentoCircularBracoDireito;
using OneDance.Core.Game.Movimentos.Gestos.Polichinelo;
using OneDance.Core.Game.Movimentos.Poses;
using System;

namespace OneDance.Core.Game.Movimentos
{
    public class Tracker : ITracker
    {
        public event EventHandler MovimentoIdentificado;
        public event EventHandler MovimentoEmProgresso;

        public Tracker(PositionTemplate positionTemplate)
        {
            EstadoAtual = TrackState.NAO_IDENTIFICADO;
            Movimento = CriarMovimento(positionTemplate, positionTemplate.Model.MargemMinima);
        }

        public Movimento Movimento
        {
            get;
            private set;
        }

        public TrackState EstadoAtual
        {
            get;
            set;
        }

        public void Rastrear(Skeleton esqueletoUsuario)
        {
            TrackState novoEstado = Movimento.Track(esqueletoUsuario);

            if (novoEstado == TrackState.IDENTIFICADO && EstadoAtual != TrackState.IDENTIFICADO)
                OnTracker(MovimentoIdentificado);

            if (novoEstado == TrackState.EM_EXECUCAO && (EstadoAtual == TrackState.EM_EXECUCAO || EstadoAtual == TrackState.NAO_IDENTIFICADO))
                OnTracker(MovimentoEmProgresso);

            EstadoAtual = novoEstado;
        }

        public void ReiniciarGesto()
        {
            if (Movimento is Gesto)
                (Movimento as Gesto).RestartTracker();
        }

        private void OnTracker(EventHandler eventCall)
        {
            eventCall?.Invoke(Movimento, new MovementValidationArgs((MovimentosEnum)Movimento.Template.Id));
        }

        private Movimento CriarMovimento(PositionTemplate tipoMovimento, double? errorRate)
        {
            switch (tipoMovimento.MovementId)
            {
                case MovimentosEnum.POLICHINELO:
                    return (Movimento)Activator.CreateInstance(typeof(Polichinelo), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.AGACHAMENTO_DIREITO:
                    return (Movimento)Activator.CreateInstance(typeof(AgachamentoDireita), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.AGACHAMENTO_ESQUERDO:
                    return (Movimento)Activator.CreateInstance(typeof(AgachamentoEsquerda), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.LEVANTAR_PERNA_DIREITA:
                    return (Movimento)Activator.CreateInstance(typeof(LevantarPernaDireita), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.LEVANTAR_PERNA_ESQUERDA:
                    return (Movimento)Activator.CreateInstance(typeof(LevantarPernaEsquerda), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.ALONGAMENTO_LATERAL_DIREITO:
                    return (Movimento)Activator.CreateInstance(typeof(ElevacaoMaoDireita), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.ALONGAMENTO_LATERAL_ESQUERDO:
                    return (Movimento)Activator.CreateInstance(typeof(ElevacaoMaoEsquerda), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.CRUZAR_AS_PERNAS:
                    return (Movimento)Activator.CreateInstance(typeof(CruzarPernas), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.LEVANTAR_BRACOS:
                    return (Movimento)Activator.CreateInstance(typeof(MaosParaCima), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.ROTACIONAR_BRACO_DIREITO:
                    return (Movimento)Activator.CreateInstance(typeof(MovimentoCircularBracoDireito), new object[] { tipoMovimento, errorRate });

                case MovimentosEnum.ROTACIONAR_BRACO_ESQUERDO:
                    return (Movimento)Activator.CreateInstance(typeof(MovimentoCircularBracoEsquerdo), new object[] { tipoMovimento, errorRate });

                default:
                    return null;
            }
        }
    }
}
