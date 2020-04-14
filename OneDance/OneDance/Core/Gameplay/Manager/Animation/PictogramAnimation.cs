using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace OneDance.Core.Gameplay.Manager.Animation
{
    public class PictogramAnimation : AbstractAnimation
    {
        public PictogramAnimation(Storyboard storyBoard, AbstractAnimation parentAnimation, int animationDuration)
            : base(storyBoard, animationDuration)
        {
            Parent = parentAnimation;
        }

        private AbstractAnimation Parent
        {
            get;
            set;
        }
        
        protected override void OnStoryboardEnd(object sender, EventArgs e)
        {
            Finish();
            Parent.Finish();
        }
    }
}
