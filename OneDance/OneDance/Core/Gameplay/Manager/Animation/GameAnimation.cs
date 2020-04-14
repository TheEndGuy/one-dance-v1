using OneDance.Core.Enums;
using OneDance.Core.Gameplay.Manager.Animation;
using System;
using System.Windows.Media.Animation;

namespace OneDance.Core.Game.Manager.Animation
{
    public class GameAnimation : AbstractAnimation
    {
        public GameAnimation(Storyboard storyBoard, Storyboard pictogramStory, Action trainingEnable, Action<ValidacaoEnum> trainingDisable)
            : base(storyBoard, 7)
        {
            EnableAction = trainingEnable;
            DisableAction = trainingDisable;
            PictogramAnimation = new PictogramAnimation(pictogramStory, this, 0);
        }

        private PictogramAnimation PictogramAnimation
        {
            get;
            set;
        }

        private Action EnableAction
        {
            get;
            set;
        }

        private Action<ValidacaoEnum> DisableAction
        {
            get;
            set;
        }

        private Action<int> EnableButtons
        {
            get;
            set;
        }
        
        public void Start(Action<int> buttonAction)
        {
            EnableButtons = buttonAction;
            Story.CurrentTimeInvalidated += OnArrivedTime;
            base.Start();
        }

        public override void Start()
        {
            Story.CurrentTimeInvalidated += OnArrivedTime;
            base.Start();
        }

        public void FinishAnimation()
        {
            Story.Pause();
            PictogramAnimation.Start();
            EnableButtons?.Invoke(1000);
        }

        public void OnArrivedTime(object sender, EventArgs e)
        {
            var currentTime = (sender as ClockGroup).CurrentTime?.Seconds;

            if (currentTime == null)
                return;

            if (currentTime >= Configuration.MIN_VALID_TIME && currentTime <= Configuration.MAX_VALID_TIME)
            {
                Story.CurrentTimeInvalidated -= OnArrivedTime;
                EnableAction?.Invoke();
            }
        }

        protected override void OnStoryboardEnd(object sender, EventArgs e)
        {
            DisableAction?.Invoke(ValidacaoEnum.INVALIDO);
        }
    }
}
