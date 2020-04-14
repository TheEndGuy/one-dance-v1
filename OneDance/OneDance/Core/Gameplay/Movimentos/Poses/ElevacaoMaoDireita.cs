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
    public class ElevacaoMaoDireita : Pose
    {
        public ElevacaoMaoDireita(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint cabeca = esqueletoUsuario.Joints[JointType.Head];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint centroQuadril = esqueletoUsuario.Joints[JointType.HipLeft];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];

            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoDireita.Position.X, cotoveloDireito.Position.X));
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoDireita.Position.X, maoEsquerda.Position.X));
            validationCheck.Add(maoDireita.Position.Y > cabeca.Position.Y);
            validationCheck.Add(maoDireita.Position.X < centroQuadril.Position.X);
            validationCheck.Add(maoEsquerda.Position.Y < centroQuadril.Position.Y);

            return validationCheck.All(x => x == true);
        }
    }
}
