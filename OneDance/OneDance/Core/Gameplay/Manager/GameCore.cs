using Microsoft.Kinect;
using OneDance.Core.Connection.KinectStart;
using OneDance.Core.Utilities;
using OneDance.Views;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OneDance.Core.Game.Manager
{
    public class GameCore
    {
        public delegate void FrameChangedDelegate(ImageBrush frame, bool hasPlayer);

        public GameCore()
        {
            Sensor = KinectStart.Instance.StartKinect();

            if (Sensor == null)
                return;
            
            Sensor.AllFramesReady += KinectFramesReady;
        }

        /// <summary>
        /// Evento usado para checar o tempo de validação de movimentos
        /// </summary>
        public event EventHandler<AllFramesReadyEventArgs> GameFrames;

        /// <summary>
        /// Evento usado para atualizar a propriedade que é usada para mostrar a image do Kinect
        /// </summary>
        /// <param name="frame"></param>
        public event FrameChangedDelegate FrameChanged;

        public KinectSensor Sensor
        {
            get;
            private set;
        }

        /// <summary>
        /// Evento que obtém dados relativos aos Frames disponibilizados pelo Sensor Kinect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KinectFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            byte[] imagem = Util.ObterImagemSensorRGB(e.OpenColorImageFrame());

            if(imagem != null)
            {
                FrameChanged?.Invoke(GetImage(imagem), e.OpenSkeletonFrame().Validation());
            }

            GameFrames?.Invoke(this, e);
        }
        
        //todo: refazer a obtenção de imagens (algo mais limpo)
        private ImageBrush GetImage(byte[] imageShow)
        {
            return new ImageBrush(BitmapSource.Create(Sensor.ColorStream.FrameWidth, Sensor.ColorStream.FrameHeight,
                                                      96, 96, PixelFormats.Bgr32, null, imageShow,
                                                      Sensor.ColorStream.FrameWidth * Sensor.ColorStream.FrameBytesPerPixel));
        }

        public void Dispose()
        {
            KinectStart.Instance.DisableKinect();

            if (Sensor == null)
                return;

            Sensor.AllFramesReady -= KinectFramesReady;
        }
    }
}

