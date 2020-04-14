using OneDance.Core.Database;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OneDance.Models.Training
{
    public class TrainingModel : ModelBase
    {
        public TrainingModel(List<MovimentosEnum> positionList)
        {
            State = GameStateEnum.Running;
            Resultado = new PositionResult(positionList);
            ButtonText = "Iniciar";

            m_currentMovement = positionList.First();
            NextImage = ActivityManager.Instance.GetPositionPictogram(m_currentMovement);
        }

        #region Property Change

        private bool nextButtonEnabled = true;

        public bool NextButtonEnabled
        {
            get { return nextButtonEnabled; }
            set { nextButtonEnabled = value; OnPropertyChanged("NextButtonEnabled"); }
        }

        private bool remakeButtonEnabled = false;

        public bool RemakeButtonEnabled
        {
            get { return remakeButtonEnabled; }
            set { remakeButtonEnabled = value; OnPropertyChanged("RemakeButtonEnabled"); }
        }

        private string buttonText;

        public string ButtonText
        {
            get { return buttonText; }
            set { buttonText = value; OnPropertyChanged("ButtonText"); }
        }

        private string nexPositionImage;

        public string NextImage
        {
            get { return nexPositionImage; }
            set { nexPositionImage = value; OnPropertyChanged("NextImage"); }
        }

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

        public GameStateEnum State
        {
            get;
            set;
        }

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

        private MovimentosEnum m_currentMovement;

        /// <summary>
        /// Altera a imagem do pictograma baseado no movimento atual
        /// </summary>
        /// <param name="nextPosition"></param>
        public void UpdateImage(MovimentosEnum nextPosition)
        {
            if (Finalizado)
                return;

            if (ButtonText.Equals("Iniciar"))
                ButtonText = "Próxima";

            if (nextPosition == MovimentosEnum.UNKNOWN)
            {
                FinalizarTreinamento();
                return;
            }

            CurrentImage = NextImage;
            NextImage = ActivityManager.Instance.GetPositionPictogram(nextPosition);
        }

        /// <summary>
        /// Ação chamada após a finalização da validação de determinado movimento
        /// </summary>
        public void OnValidationFinish(object sender, ValidacaoEnum validationType)
        {
            Resultado.AdicionarTotal(validationType, m_currentMovement);
            FeedbackImage = validationType == ValidacaoEnum.VALIDO ? "/Images/Sequencia/Feedback/Acerto.png" : "/Images/Sequencia/Feedback/Falha.png";

            if(!NextImage.Equals(""))
                m_currentMovement = ActivityManager.Instance.GetMovement(NextImage);
        }

        /// <summary>
        /// Finaliza o treinamento, alterando o estado para finalizado e removendo a próxima imagem de pose
        /// </summary>
        private void FinalizarTreinamento()
        {
            if (Finalizado)
                return;

            Finalizado = true;
            CurrentImage = NextImage;
            NextImage = "";
        }

        public async void ButtonsChange(int delay)
        {
            if (delay == 0)
            {
                InternalButtonsChange();
                return;
            }

            await Task.Delay(delay);

            await Application.Current.Dispatcher.BeginInvoke(
                    new Action(() => InternalButtonsChange()),
                    DispatcherPriority.Background);
        }

        /// <summary>
        /// Manipula o estado dos botões 'Próximo' e 'Refazer'
        /// </summary>
        private void InternalButtonsChange()
        {
            //caso o jogo for pausado antes do dispatcher terminar o envio da solicitação
            if (State == GameStateEnum.Paused)
                return;

            if (Finalizado)
                NextButtonEnabled = false;

            else
                NextButtonEnabled = !NextButtonEnabled;

            if (Resultado.PrimeiraValidacao)
                return;

            else
                RemakeButtonEnabled = !RemakeButtonEnabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void ButtonState(bool state)
        {
            NextButtonEnabled = state;
            RemakeButtonEnabled = state;
        }

        /// <summary>
        /// Reseta o treinamento para iniciar um novo jogo
        /// </summary>
        public void ResetTraining()
        {
            RemakeButtonEnabled = false;

           // if (Resultado.PrimeiraValidacao) //Refazer o treinamento porém nem começou ? x)
           //     return;

            Finalizado = false;
            m_currentMovement = Resultado.Total.Keys.First();
            NextImage = ActivityManager.Instance.GetPositionPictogram(m_currentMovement);

            CurrentImage = "";
            ButtonText = "Iniciar";

            Resultado.Reset();
        }
    }
}
