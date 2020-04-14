using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class LevantarPernaEsquerda : Pose
    {
        public LevantarPernaEsquerda(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint joelhoEsquerdo = esqueletoUsuario.Joints[JointType.KneeLeft];
            Joint peEsquerdo = esqueletoUsuario.Joints[JointType.FootLeft];

            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, peEsquerdo.Position.X, joelhoEsquerdo.Position.X));
            validationCheck.Add(Util.CompararComMargemErro(0.10, peEsquerdo.Position.Y, joelhoEsquerdo.Position.Y));
            validationCheck.Add(peEsquerdo.Position.Z < joelhoEsquerdo.Position.Z);

            return validationCheck.All(x => x == true);
        }
    }
}
