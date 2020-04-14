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
    public class CruzarPernas : Pose
    {
        public CruzarPernas(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint joelhoEsquerdo = esqueletoUsuario.Joints[JointType.KneeLeft];
            Joint joelhoDireito = esqueletoUsuario.Joints[JointType.KneeRight];
            Joint peEsquerdo = esqueletoUsuario.Joints[JointType.FootLeft];
            Joint peDireito = esqueletoUsuario.Joints[JointType.FootRight];

            validationCheck.Add(peDireito.Position.Y < joelhoDireito.Position.Y);
            validationCheck.Add(peEsquerdo.Position.Y < joelhoEsquerdo.Position.Y);
            validationCheck.Add(peEsquerdo.Position.X > peDireito.Position.X);
            validationCheck.Add(peDireito.Position.X < peEsquerdo.Position.X);

            return validationCheck.All(x => x == true);
        }
    }
}
