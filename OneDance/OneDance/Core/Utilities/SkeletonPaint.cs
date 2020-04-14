using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OneDance.Core.Utilities
{
    public class SkeletonPaint
    {
        public SkeletonPaint(KinectSensor kinect)
        {
            Kinect = kinect;
        }

        private KinectSensor Kinect
        {
            get;
            set;
        }

        public void DesenharArticulacao(Joint articulacao, Canvas canvasParaDesenhar)
        {
            int diametroArticulacao = articulacao.JointType == JointType.Head ? 50 : 10;
            Ellipse objetoArticulacao = CriarComponenteVisualArticulacao(diametroArticulacao, 4, Brushes.Red);

            ColorImagePoint posicaoArticulacao = ConverterCoordenadasArticulacao(articulacao, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);

            double deslocamentoHorizontal = posicaoArticulacao.X - objetoArticulacao.Width / 2;
            double deslocamentoVertical = (posicaoArticulacao.Y - objetoArticulacao.Height / 2);

            if (deslocamentoVertical >= 0 && deslocamentoVertical < canvasParaDesenhar.ActualHeight && deslocamentoHorizontal >= 0 && deslocamentoHorizontal < canvasParaDesenhar.ActualWidth)
            {
                Canvas.SetLeft(objetoArticulacao, deslocamentoHorizontal);
                Canvas.SetTop(objetoArticulacao, deslocamentoVertical);
                Panel.SetZIndex(objetoArticulacao, 100);

                canvasParaDesenhar.Children.Add(objetoArticulacao);
            }
        }

        public void DesenharOsso(Joint articulacaoOrigem, Joint articulacaoDestino, Canvas canvasParaDesenhar)
        {
            ColorImagePoint posicaoArticulacaoOrigem = ConverterCoordenadasArticulacao(articulacaoOrigem, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);
            ColorImagePoint posicaoArticulacaoDestino = ConverterCoordenadasArticulacao(articulacaoDestino, canvasParaDesenhar.ActualWidth, canvasParaDesenhar.ActualHeight);

            Line objetoOsso = CriarComponenteVisualOsso(4, Brushes.Green, posicaoArticulacaoOrigem.X, posicaoArticulacaoOrigem.Y, posicaoArticulacaoDestino.X, posicaoArticulacaoDestino.Y);

            if (Math.Max(objetoOsso.X1, objetoOsso.X2) < canvasParaDesenhar.ActualWidth
                && Math.Min(objetoOsso.X1, objetoOsso.X2) > 0 && Math.Max(objetoOsso.Y1, objetoOsso.Y2) < canvasParaDesenhar.ActualHeight
                && Math.Min(objetoOsso.Y1, objetoOsso.Y2) > 0)
            {
                canvasParaDesenhar.Children.Add(objetoOsso);
            }
        }

        private ColorImagePoint ConverterCoordenadasArticulacao(Joint articulacao, double larguraCanvas, double alturaCanvas)
        {
            ColorImagePoint posicaoArticulacao = Kinect.CoordinateMapper.MapSkeletonPointToColorPoint(articulacao.Position, Kinect.ColorStream.Format);

            posicaoArticulacao.X = (int)(posicaoArticulacao.X * larguraCanvas) / Kinect.ColorStream.FrameWidth;
            posicaoArticulacao.Y = (int)(posicaoArticulacao.Y * alturaCanvas) / Kinect.ColorStream.FrameHeight;

            return posicaoArticulacao;
        }

        private Ellipse CriarComponenteVisualArticulacao(int diametroArticulacao, int larguraDesenho, Brush corDesenho)
        {
            Ellipse objetoArticulacao = new Ellipse()
            {
                Height = diametroArticulacao,
                Width = diametroArticulacao,
                StrokeThickness = larguraDesenho,
                Stroke = corDesenho
            };

            return objetoArticulacao;
        }

        private Line CriarComponenteVisualOsso(int larguraDesenho, Brush corDesenho, double origemX, double origemY, double destinoX, double destinoY)
        {
            Line objetoOsso = new Line()
            {
                StrokeThickness = larguraDesenho,
                Stroke = corDesenho
            };

            objetoOsso.X1 = origemX;
            objetoOsso.X2 = destinoX;

            objetoOsso.Y1 = origemY;
            objetoOsso.Y2 = destinoY;

            return objetoOsso;
        }
    }
}
