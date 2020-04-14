using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;

namespace OneDance.Core.Game.Movimentos
{
    public abstract class Movimento
    {
        public Movimento()
        {
        }

        public Movimento(PositionTemplate positionTemplate, double errorRate)
        {
            Template = positionTemplate;
            ErrorRate = errorRate;
        }

        public PositionTemplate Template
        {
            get;
            set;
        }

        /// <summary>
        /// Número de quadros necessários para a validação do movimento
        /// </summary>
        public int FrameCounter
        {
            get;
            set;
        }

        /// <summary>
        /// Taxa de erro
        /// </summary>
        public double ErrorRate
        {
            get;
            set;
        }

        public abstract TrackState Track(Skeleton esqueletoUsuario);

        protected abstract bool ValidPosition(Skeleton esqueletoUsuario);
    }
}
