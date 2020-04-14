using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;

namespace OneDance.Core.Game.Movimentos
{
    public abstract class Pose : Movimento
    {
        public Pose()
        {
        }

        public Pose(double errorRate)
        {
            ErrorRate = errorRate;
        }

        public Pose(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
        }

        /// <summary>
        /// Quadro que identificará quando a pose será validada
        /// </summary>
        protected int IdentifierFrame
        {
            get;
            set;
        }

        /// <summary>
        /// Porcentagem atual da identificação da pose
        /// </summary>
        public int ProgressPercent
        {
            get { return FrameCounter * 100 / IdentifierFrame; }
        }

        /// <summary>
        /// Retorna o estado de rastreamento da pose atual
        /// </summary>
        /// <param name="esqueletoUsuario"></param>
        /// <returns></returns>
        public override TrackState Track(Skeleton esqueletoUsuario)
        {
            TrackState estado;

            if (esqueletoUsuario != null && ValidPosition(esqueletoUsuario))
            {
                if (IdentifierFrame == FrameCounter)
                    estado = TrackState.IDENTIFICADO;

                else
                {
                    estado = TrackState.EM_EXECUCAO;
                    FrameCounter += 1;
                }
            }
            else
            {
                estado = TrackState.NAO_IDENTIFICADO;
                FrameCounter = 0;
            }

            return estado;
        }
    }
}

