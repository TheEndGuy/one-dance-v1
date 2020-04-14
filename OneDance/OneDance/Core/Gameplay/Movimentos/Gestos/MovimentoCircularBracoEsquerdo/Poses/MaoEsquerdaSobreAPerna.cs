using Microsoft.Kinect;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos.Gestos
{
    public class MaoEsquerdaSobreAPerna : Pose
    {
        public MaoEsquerdaSobreAPerna(double errorRate)
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
            Joint joelhoEsquerdo = esqueletoUsuario.Joints[JointType.KneeLeft];

            validationCheck.Add(centroOmbros.Position.Y > maoEsquerda.Position.Y);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.Z, joelhoEsquerdo.Position.Z));
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoEsquerda.Position.Y, joelhoEsquerdo.Position.Y));
            validationCheck.Add(maoEsquerda.Position.Z < centroOmbros.Position.Z);
            validationCheck.Add(maoEsquerda.Position.X > joelhoEsquerdo.Position.X);
            validationCheck.Add(cotoveloEsquerdo.Position.Z > maoEsquerda.Position.Z);

            return validationCheck.All(x => x == true);
        }
    }
}
