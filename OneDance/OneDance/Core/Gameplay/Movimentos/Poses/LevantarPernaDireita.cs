using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Utilities;
using OneDance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OneDance.Core.Game.Movimentos.Poses
{
    public class LevantarPernaDireita : Pose
    {
        public LevantarPernaDireita(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            IdentifierFrame = 1;
        }

        protected override bool ValidPosition(Skeleton esqueletoUsuario)
        {
            List<bool> validationCheck = new List<bool>();

            Joint peDireito = esqueletoUsuario.Joints[JointType.FootRight];
            Joint joelhoDireito = esqueletoUsuario.Joints[JointType.KneeRight];

            validationCheck.Add(Util.CompararComMargemErro(ErrorRate, peDireito.Position.X, joelhoDireito.Position.X));
            validationCheck.Add(Util.CompararComMargemErro(0.10, peDireito.Position.Y, joelhoDireito.Position.Y));
            validationCheck.Add(peDireito.Position.Z < joelhoDireito.Position.Z);
            
            return validationCheck.All(x => x == true);
        }
    }
}
