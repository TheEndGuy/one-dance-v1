using OneDance.Core.Database;
using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Game
{
    public class GameModel : ModelBase
    {
        public GameModel(List<MovimentosEnum> positionList)
        {
            Resultado = new PositionResult(positionList);
            m_currentMovement = positionList.First();
        }

        #region Property Changed

        private string currentPositionImage;

        public string CurrentImage
        {
            get { return currentPositionImage; }
            set { currentPositionImage = value; OnPropertyChanged("CurrentImage"); }
        }

        private string feedbackImage;

        public string FeedbackImage
        {
            get { return feedbackImage; }
            set { feedbackImage = value; OnPropertyChanged("FeedbackImage"); }
        }

        #endregion

        public PositionResult Resultado
        {
            get;
            set;
        }

        public bool Finalizado
        {
            get;
            set;
        }

        public MovimentosEnum m_currentMovement;

        /// <summary>
        /// Ação chamada após a finalização da validação de determinado movimento
        /// </summary>
        public void OnValidationFinish(ValidacaoEnum validationType)
        {
            Resultado.AdicionarTotal(validationType, m_currentMovement);
            FeedbackImage = validationType == ValidacaoEnum.VALIDO ? "Images/Sequencia/Feedback/Acerto/Acertou0001.png" : "Images/Sequencia/Feedback/Erro/Errou0001.png";
        }

        /// <summary>
        /// Altera a imagem do pictograma baseado no movimento atual
        /// </summary>
        /// <param name="nextPosition"></param>
        public void UpdateImage(MovimentosEnum nextPosition)
        {
            if (Finalizado)
                return;
            
            if (nextPosition == MovimentosEnum.DISABLE)
            {
                Finalizado = true;
                return;
            }

            m_currentMovement = nextPosition;
            CurrentImage = ActivityManager.Instance.GetPositionPictogram(nextPosition);
        }

        /// <summary>
        ///
        /// </summary>
        public void ResetGame()
        {
            Finalizado = false;
            m_currentMovement = Resultado.Total.Keys.First();

            CurrentImage = ActivityManager.Instance.GetPositionPictogram(m_currentMovement);
            CurrentImage = "";

            Resultado.Reset();
        }
    }
}
