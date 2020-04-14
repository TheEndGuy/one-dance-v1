using Microsoft.Kinect;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Movimentos.Gestos
{
    public class MaoEsquerdaParaCima : Pose
    {
        public MaoEsquerdaParaCima(double errorRate)
            : base(errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint centroOmbros = esqueletoUsuario.Joints[JointType.ShoulderCenter];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint cotoveloEsquerdo = esqueletoUsuario.Joints[JointType.ElbowLeft];
            Joint cabeca = esqueletoUsuario.Joints[JointType.Head];

            validationCheck.Add(centroOmbros.Position.Y < maoEsquerda.Position.Y);
            validationCheck.Add(maoEsquerda.Position.Y > cabeca.Position.Y);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.Z, cabeca.Position.Z));
            validationCheck.Add(maoEsquerda.Position.X < cotoveloEsquerdo.Position.X);
            validationCheck.Add(centroOmbros.Position.Y < cotoveloEsquerdo.Position.Y);

            return validationCheck.All(x => x == true);
        }
    }
}
