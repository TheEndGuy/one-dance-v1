using System;
using System.Windows.Media;

namespace OneDance.Core.Gameplay.Manager.Events
{
    public class ImageArgs : EventArgs
    {
        public ImageArgs(ImageBrush image)
        {
            Image = image;
        }

        public ImageBrush Image
        {
            get;
            set;
        }
    }
}
