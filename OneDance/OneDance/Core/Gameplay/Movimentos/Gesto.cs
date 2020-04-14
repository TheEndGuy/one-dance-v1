using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos
{
    public abstract class Gesto : Movimento
    {
        public Gesto(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
        }

        /// <summary>
        /// Lista contendo as posições necessárias para validação do gesto
        /// </summary>
        protected LinkedList<GestureKeyFrame> KeyFrames
        {
            get;
            set;
        }

        /// <summary>
        /// Atual posição (Frame)
        /// </summary>
        protected LinkedListNode<GestureKeyFrame> CurrentKeyFrame
        {
            get;
            set;
        }

        /// <summary>
        /// Contador contendo o número de etapas já concluídas
        /// </summary>
        public int StepsCount
        {
            get;
            set;
        }

        /// <summary>
        /// Contador contendo o número total de etapas
        /// </summary>
        private int StepsCounter
        {
            get { return KeyFrames.Count; }
        }

        /// <summary>
        /// Porcentagem atual da identificação do gesto
        /// </summary>
        public int ProgressPercent
        {
            get { return StepsCount * 100 / StepsCounter; }
        }

        /// <summary>
        /// Estado atual da validação do gesto
        /// </summary>
        private TrackState Estado
        {
            get;
            set;
        }

        public virtual void StartKeyFrames()
        {
            FrameCounter = 0;
            CurrentKeyFrame = KeyFrames.First;
        }

        /// <summary>
        /// Retorna o estado de rastreamento do gesto atual
        /// </summary>
        /// <param name="esqueletoUsuario"></param>
        /// <returns></returns>
        public override TrackState Track(Skeleton esqueletoUsuario)
        {   
            if(esqueletoUsuario == null)
                RestartTracker();

            if (ValidPosition(esqueletoUsuario))
            {
                Estado = TrackState.EM_EXECUCAO;

                if (CurrentKeyFrame.Value == KeyFrames.Last.Value)
                    Estado = TrackState.IDENTIFICADO;

                else
                {
                    if (FrameCounter >= CurrentKeyFrame.Value.QuadroLimiteInferior && FrameCounter <= CurrentKeyFrame.Value.QuadroLimiteSuperior)
                        NextKeyFrame();

                    else if (FrameCounter < CurrentKeyFrame.Value.QuadroLimiteInferior)
                        Tracking();

                    else if (FrameCounter > CurrentKeyFrame.Value.QuadroLimiteSuperior)
                        RestartTracker();
                }
            }

            else if (CurrentKeyFrame.Value.QuadroLimiteSuperior < FrameCounter)
                RestartTracker();

            else
                Tracking();

            var index = KeyFrames.TakeWhile(n => n != CurrentKeyFrame.Value).Count();

            ConsoleLog.WriteLog("("+ index + ")" + CurrentKeyFrame.Value.PoseChave.ToString() + " / " + Estado
                                + " / " + FrameCounter , ConsoleStatesEnum.NOTICE);

            return Estado;
        }

        /// <summary>
        /// Avança para o próximo quadro (Pose) para validação do gesto
        /// </summary>
        private void NextKeyFrame()
        {
            Estado = TrackState.EM_EXECUCAO;
            FrameCounter = 0;
            CurrentKeyFrame = CurrentKeyFrame.Next;
            StepsCount++;
        }

        /// <summary>
        /// Reinicia a validação do gesto
        /// </summary>
        public void RestartTracker()
        {
            FrameCounter = 0;
            CurrentKeyFrame = KeyFrames.First;
            StepsCount = 0;
            Estado = TrackState.NAO_IDENTIFICADO;
        }

        /// <summary>
        /// Continua rastreando o gesto, somando o contador de frames
        /// </summary>
        private void Tracking()
        {
            FrameCounter++;
            Estado = TrackState.EM_EXECUCAO;
        }
    }
}
