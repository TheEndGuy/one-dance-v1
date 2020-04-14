using Microsoft.Kinect;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos.Gestos.MovimentoCircularBracoDireito
{
    public class MaoDireitaSobreAPerna : Pose
    {
        public MaoDireitaSobreAPerna(double errorRate)
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
            Joint joelhoDireito = esqueletoUsuario.Joints[JointType.KneeRight];

            validationCheck.Add(centroOmbros.Position.Y > maoDireita.Position.Y);
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoDireita.Position.Z, joelhoDireito.Position.Z));
            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, maoDireita.Position.Y, joelhoDireito.Position.Y));
            validationCheck.Add(maoDireita.Position.Z < centroOmbros.Position.Z);
            validationCheck.Add(maoDireita.Position.X < joelhoDireito.Position.X);
            validationCheck.Add(cotoveloDireito.Position.Z > maoDireita.Position.Z);

            return validationCheck.All(x => x == true);
        }
    }
}
