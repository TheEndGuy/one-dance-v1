using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class MaosParaCima : Pose
    {
        public MaosParaCima(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint centroOmbros = esqueletoUsuario.Joints[JointType.ShoulderCenter];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint cotoveloEsquerdo = esqueletoUsuario.Joints[JointType.ElbowLeft];
            Joint cabeca = esqueletoUsuario.Joints[JointType.Head];

            validationCheck.Add(centroOmbros.Position.Y < maoDireita.Position.Y);
            //validationCheck.Add(maoDireita.Position.X > cotoveloDireito.Position.X);
            validationCheck.Add(centroOmbros.Position.Y < cotoveloDireito.Position.Y);
            validationCheck.Add(centroOmbros.Position.Y < cotoveloEsquerdo.Position.Y);
            validationCheck.Add(centroOmbros.Position.Y < maoEsquerda.Position.Y);
            //validationCheck.Add(maoEsquerda.Position.X < cotoveloDireito.Position.X);

            return validationCheck.All(x => x == true);
        }
    }
}
