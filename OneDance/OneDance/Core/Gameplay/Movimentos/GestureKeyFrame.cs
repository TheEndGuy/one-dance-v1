using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos
{
    public class GestureKeyFrame
    {
        public GestureKeyFrame(Pose poseChave, int limiteInferior, int limiteSuperior)
        {
            PoseChave = poseChave;
            QuadroLimiteInferior = limiteInferior;
            QuadroLimiteSuperior = limiteSuperior;
        }

        public int QuadroLimiteInferior
        {
            get;
            private set;
        }

        public int QuadroLimiteSuperior
        {
            get;
            private set;
        }

        public Pose PoseChave
        {
            get;
            private set;
        }
    }
}
