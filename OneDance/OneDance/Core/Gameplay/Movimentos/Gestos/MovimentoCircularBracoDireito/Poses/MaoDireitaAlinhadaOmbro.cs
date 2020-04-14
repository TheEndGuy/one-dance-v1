using Microsoft.Kinect;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Gestos.MovimentoCircularBracoDireito
{
    public class MaoDireitaAlinhadaOmbro : Pose
    {
        public MaoDireitaAlinhadaOmbro(double errorRate)
            : base(errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint centroOmbros = esqueletoUsuario.Joints[JointType.ShoulderCenter];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];
            Joint cabeca = esqueletoUsuario.Joints[JointType.Head];

            validationCheck.Add(centroOmbros.Position.Y < maoDireita.Position.Y);
            validationCheck.Add(maoDireita.Position.Y < cabeca.Position.Y);
            validationCheck.Add(maoDireita.Position.Z > centroOmbros.Position.Z);
            validationCheck.Add(maoDireita.Position.X > cotoveloDireito.Position.X);
            validationCheck.Add(centroOmbros.Position.Y < cotoveloDireito.Position.Y);

            return validationCheck.All(x => x == true);
        }
    }
}
