using OneDance.Core.Enums;
using System.Windows.Media.Animation;

namespace OneDance.Core.Gameplay.Manager.Animation
{
    public class FeedbackAnimation
    {
        public FeedbackAnimation(Storyboard sucessAnimation, Storyboard failureAnimation)
        {
            SucessAnimation = new BasicAnimation(sucessAnimation, 0);
            FailureAnimation = new BasicAnimation(failureAnimation, 0);
        }

        public BasicAnimation SucessAnimation
        {
            get;
            set;
        }

        public BasicAnimation FailureAnimation
        {
            get;
            set;
        }

        public void Execute(ValidacaoEnum validationType)
        {
            if (validationType == ValidacaoEnum.VALIDO)
                SucessAnimation.Start();

            else if (validationType == ValidacaoEnum.INVALIDO)
                FailureAnimation.Start();
        }
    }
}
