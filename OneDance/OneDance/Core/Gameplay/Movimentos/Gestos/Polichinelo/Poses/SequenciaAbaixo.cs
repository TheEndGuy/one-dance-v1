using Microsoft.Kinect;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos.Gestos.Polichinelo
{
    public class SequenciaAbaixo : Pose
    {
        public SequenciaAbaixo(double errorRate)
            : base(errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            //Pernas
            Joint peDireito = esqueletoUsuario.Joints[JointType.FootRight];
            Joint peEsquerdo = esqueletoUsuario.Joints[JointType.FootLeft];
            Joint centroQuadril = esqueletoUsuario.Joints[JointType.HipCenter];
            Joint joelhoDireito = esqueletoUsuario.Joints[JointType.KneeRight];
            Joint joelhoEsquerdo = esqueletoUsuario.Joints[JointType.KneeLeft];

            //Braços
            Joint centroOmbro = esqueletoUsuario.Joints[JointType.ShoulderCenter];
            Joint maoDireita = esqueletoUsuario.Joints[JointType.HandRight];
            Joint cotoveloDireito = esqueletoUsuario.Joints[JointType.ElbowRight];
            Joint maoEsquerda = esqueletoUsuario.Joints[JointType.HandLeft];
            Joint cotoveloEsquerdo = esqueletoUsuario.Joints[JointType.ElbowLeft];

            //Pernas
            validationCheck.Add(joelhoDireito.Position.X - joelhoEsquerdo.Position.X < 0.25);
            validationCheck.Add(peDireito.Position.X - peEsquerdo.Position.X < 0.25);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, peDireito.Position.Z, centroQuadril.Position.Z));
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, peEsquerdo.Position.Z, centroQuadril.Position.Z));

            //Braços
            validationCheck.Add(centroOmbro.Position.Y > maoDireita.Position.Y);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoDireita.Position.Z, centroOmbro.Position.Z));
            validationCheck.Add(maoDireita.Position.X > cotoveloDireito.Position.X);
            validationCheck.Add(centroOmbro.Position.Y > cotoveloDireito.Position.Y);
            validationCheck.Add(centroOmbro.Position.Y > cotoveloEsquerdo.Position.Y);
            validationCheck.Add(centroOmbro.Position.Y > maoEsquerda.Position.Y);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.Z, centroOmbro.Position.Z));
            validationCheck.Add(maoEsquerda.Position.X < cotoveloDireito.Position.X);

            return validationCheck.All(x => x == true);
        }
    }
}
