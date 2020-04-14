using Microsoft.Kinect;
using OneDance.Core.Game.Movimentos;
using OneDance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneDance.Core.Utilities
{
    public static class Util
    {
        public static double GetDistance(SkeletonPoint point)
        {
            return Math.Sqrt(
                point.X * point.X +
                point.Y * point.Y +
                point.Z * point.Z
            );
        }

        public static bool CompararComMargemErro(double margemErro, double valor1, double valor2)
        {
            return ((valor1 >= (valor2 - margemErro)) && (valor1 <= (valor2 + margemErro)));
        }

        public static string RemoverAcentos(string str)
        {
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
                str = str.Replace(acentos[i], semAcento[i]);

            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
                str = str.Replace(caracteresEspeciais[i], "");

            str = str.Replace("^\\s+", "");
            str = str.Replace("\\s+$", "");
            str = str.Replace("\\s+", "");
            str = str.Replace(" ", string.Empty);

            return str;
        }

        public static byte[] GetRgbImage(ColorImageFrame colorFrame)
        {
            if (colorFrame == null)
                return null;

            using (colorFrame)
            {
                byte[] bytesImagem = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(bytesImagem);

                return bytesImagem;
            }
        }
        
        public static void Rastrear(SkeletonFrame skeletonFrame, ITracker[] rastreadoresCheck)
        {
            if (skeletonFrame == null)
                return;

            Skeleton esqueletoUsuario = skeletonFrame.GetTrackedSkeletons()?.First();

            if (esqueletoUsuario == null)
                return;

            foreach (ITracker rastreador in rastreadoresCheck)
                rastreador.Rastrear(esqueletoUsuario);
        }

        public static byte[] ObterImagemSensorRGB(ColorImageFrame quadro)
        {
            if (quadro == null)
                return null;

            using (quadro)
            {
                byte[] bytesImagem = new byte[quadro.PixelDataLength];

                quadro.CopyPixelDataTo(bytesImagem);

                return bytesImagem;
            }
        }
    }
}
