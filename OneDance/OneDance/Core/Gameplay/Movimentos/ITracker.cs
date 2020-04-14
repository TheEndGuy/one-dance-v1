using Microsoft.Kinect;
using OneDance.Core.Enums;

namespace OneDance.Core.Game.Movimentos
{
    public interface ITracker
    {
        TrackState EstadoAtual
        {
            get;
        }

        void Rastrear(Skeleton esqueletoUsuario);
    }
}
