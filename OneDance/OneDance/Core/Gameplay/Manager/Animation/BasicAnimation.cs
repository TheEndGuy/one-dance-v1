using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace OneDance.Core.Gameplay.Manager.Animation
{
    public class BasicAnimation : AbstractAnimation
    {
        public BasicAnimation(Storyboard storyBoard, int animationDuration)
            : base(storyBoard, animationDuration)
        {
        }

        protected override void OnStoryboardEnd(object sender, EventArgs e)
        {
            Finish();
        }
    }
}
