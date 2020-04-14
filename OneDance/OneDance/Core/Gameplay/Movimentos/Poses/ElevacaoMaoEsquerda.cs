using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class ElevacaoMaoEsquerda : Pose
    {
        public ElevacaoMaoEsquerda(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint cabeca = esqueletoUsuario.Joints[JointType.Head];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloEsquerdo = esqueletoUsuario.Joints[JointType.ElbowLeft];
            Joint centroQuadril = esqueletoUsuario.Joints[JointType.HipLeft];

            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.X, cotoveloEsquerdo.Position.X));
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.X, maoDireita.Position.X));
            validationCheck.Add(maoEsquerda.Position.Y > cabeca.Position.Y);
            validationCheck.Add(maoEsquerda.Position.X > centroQuadril.Position.X);
            validationCheck.Add(maoDireita.Position.Y < centroQuadril.Position.Y);

            return validationCheck.All(x => x == true);
        }
    }
}
