using OneDance.Core.Gameplay.Manager.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace OneDance.Views
{
    /// <summary>
    /// Interação lógica para TrainingView.xam
    /// </summary>
    public partial class TrainingView : UserControl
    {
        /// <summary>
        /// Animação de entrada do Pictograma
        /// </summary>
        private static Storyboard EnteringStory;

        /// <summary>
        /// Anmação de finalização do Pictograma
        /// </summary>
        private static Storyboard FinishStory;

        /// <summary>
        /// Animação do feedback do usuário (sucesso)
        /// </summary>
        private static Storyboard FeedbackSucessStory;

        /// <summary>
        /// Animação do feedback do usuário (falha)
        /// </summary>
        private static Storyboard FeedbackFailureStory;

        public TrainingView()
        {
            InitializeComponent();

            EnteringStory = TryFindResource("EnteringPosition") as Storyboard;
            FinishStory = TryFindResource("FinishAnimation") as Storyboard;

            FeedbackSucessStory = TryFindResource("FeedbackAcerto") as Storyboard;
            FeedbackFailureStory = TryFindResource("FeedbackErro") as Storyboard;
        }

        /// <summary>
        /// Retorna a animação de acordo com o parâmetro solicitado
        /// </summary>
        /// <param name="storyboardName"></param>
        /// <returns></returns>
        public static Storyboard GetStoryboard(string storyboardName)
        {
            if (storyboardName.Equals("EnteringPosition"))
                return EnteringStory;

            else if (storyboardName.Equals("FinishAnimation"))
                return FinishStory;

            else if (storyboardName.Equals("FeedbackAcerto"))
                return FeedbackSucessStory;

            else if (storyboardName.Equals("FeedbackErro"))
                return FeedbackFailureStory;

            return null;
        }
    }
}
