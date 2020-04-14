using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class AgachamentoEsquerda : Pose
    {
        public AgachamentoEsquerda(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint peDireito = esqueletoUsuario.Joints[JointType.FootRight];
            Joint joelhoDireito = esqueletoUsuario.Joints[JointType.KneeRight];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint quadrilCentro = esqueletoUsuario.Joints[JointType.HipCenter];
            Joint cotoveloEsquerdo = esqueletoUsuario.Joints[JointType.ElbowLeft];

            validationCheck.Add(Util.CompararComMargemErro(0.50, maoEsquerda.Position.Y, peDireito.Position.Y));
            validationCheck.Add(maoEsquerda.Position.X > joelhoDireito.Position.X);
            validationCheck.Add(Util.CompararComMargemErro(0.15, cotoveloEsquerdo.Position.Y, quadrilCentro.Position.Y));


            return validationCheck.All(x => x == true);
        }
    }
}
