using Microsoft.Kinect;
using OneDance.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneDance.Core.Utilities
{
    public static class SkeletonFrameExtension
    {
        /// <summary>
        /// Retorna o total de jogadores reconhecidos pelo Kinect
        /// </summary>
        /// <param name="skeletonFrame"></param>
        /// <returns></returns>
        public static IEnumerable<Skeleton> GetTrackedSkeletons(this SkeletonFrame skeletonFrame)
        {
            if (skeletonFrame == null)
                return null;

            Skeleton[] skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
            skeletonFrame.CopySkeletonDataTo(skeletons);

            IEnumerable<Skeleton> trackedSkeletons = skeletons.Where(x => x.TrackingState == SkeletonTrackingState.Tracked);

            if (trackedSkeletons.Count() > 0)
                return trackedSkeletons;

            return null;
        }

        /// <summary>
        /// Retorna verdadeiro se o Kinect estiver reconhecendo apenas um jogador e o mesmo estiver na distância mínima
        /// </summary>
        /// <param name="skeletonFrame"></param>
        /// <returns></returns>
        public static bool Validation(this SkeletonFrame skeletonFrame)
        {
            var skeletonsTracked = skeletonFrame.GetTrackedSkeletons();

            if (skeletonsTracked == null || skeletonsTracked.Count() > 1)
                return false;

            if (skeletonsTracked.First().Joints[JointType.Spine].TrackingState == JointTrackingState.NotTracked)
                return false;

            var distance = Util.GetDistance(skeletonsTracked.First().Joints[JointType.Spine].Position);

            if (distance >= Configuration.DISTANCE_CHECK)
                return true;

            return false;
        }
    }
}
