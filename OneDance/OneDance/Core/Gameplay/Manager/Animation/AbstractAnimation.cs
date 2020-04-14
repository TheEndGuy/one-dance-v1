using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace OneDance.Core.Gameplay.Manager.Animation
{
    public abstract class AbstractAnimation
    {
        public AbstractAnimation(Storyboard storyBoard, int animationDuration = 0)
        {
            Story = storyBoard;
            State = StoryboardState.Stopped;

            if (animationDuration > 0)
                Story.Duration = new Duration(new TimeSpan(0, 0, animationDuration));
        }

        public bool Running => State == StoryboardState.Running;

        public Storyboard Story
        {
            get;
            set;
        }

        public StoryboardState State
        {
            get;
            set;
        }

        public virtual void Start()
        {
            State = StoryboardState.Running;

            Story.Completed += OnStoryboardEnd;
            Story.Begin();
        }

        public void Pause()
        {
            if (State == StoryboardState.Paused)
                return;

            else if (State == StoryboardState.Running)
            {
                State = StoryboardState.Paused;
                Story.Pause();
            }
        }

        public void Resume()
        {
            if (State == StoryboardState.Running)
                return;

            if (State == StoryboardState.Paused)
            {
                State = StoryboardState.Running;
                Story.Resume();
            }
        }

        public virtual void Finish()
        {
            Story.Stop();
            Story.Remove();
            Story.Completed -= OnStoryboardEnd;

            State = StoryboardState.Stopped;
        }

        protected abstract void OnStoryboardEnd(object sender, EventArgs e);
    }
}
