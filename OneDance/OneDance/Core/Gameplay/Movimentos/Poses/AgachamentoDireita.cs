using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class AgachamentoDireita : Pose
    {
        public AgachamentoDireita(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint peEsquerdo = esqueletoUsuario.Joints[JointType.FootLeft];
            Joint joelhoesquerdo = esqueletoUsuario.Joints[JointType.KneeLeft];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint quadrilCentro = esqueletoUsuario.Joints[JointType.HipCenter];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];

            validationCheck.Add(Util.CompararComMargemErro(0.50, maoDireita.Position.Y, peEsquerdo.Position.Y));
            validationCheck.Add(maoDireita.Position.X < joelhoesquerdo.Position.X);
            validationCheck.Add(Util.CompararComMargemErro(0.15, cotoveloDireito.Position.Y, quadrilCentro.Position.Y));

            return validationCheck.All(x => x == true);
        }
    }
}
