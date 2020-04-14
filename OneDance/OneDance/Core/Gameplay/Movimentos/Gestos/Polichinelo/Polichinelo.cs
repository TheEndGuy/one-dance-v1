using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Movimentos.Gestos.Polichinelo
{
    public class Polichinelo : Gesto
    {
        public Polichinelo(PositionTemplate positionTemplate, double errorRate)
            : base(positionTemplate, errorRate)
        {
            StartKeyFrames();
        }

        public override void StartKeyFrames()
        {
            KeyFrames = new LinkedList<GestureKeyFrame>();
            KeyFrames.AddFirst(new GestureKeyFrame(new SequenciaAcima(ErrorRate), 0, 0));;
            KeyFrames.AddLast(new GestureKeyFrame(new SequenciaAbaixo(ErrorRate), 2, 25));

            base.StartKeyFrames();
        }

        protected override bool ValidPosition(Skeleton userSkeleton)
        {
            return CurrentKeyFrame.Value.PoseChave.Track(userSkeleton) == TrackState.IDENTIFICADO;
        }
    }
}
