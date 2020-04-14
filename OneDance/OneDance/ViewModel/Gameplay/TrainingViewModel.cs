using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using OneDance.Core.Game.Treinamento;
using OneDance.Models;
using OneDance.Models.Training;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace OneDance.ViewModel
{
    public class TrainingViewModel : AbstractGameViewModel
    {
        public TrainingViewModel(List<PositionTemplate> positionList)
            : base(DialogTypeEnum.TRAINING, positionList)
        {
            Model = new TrainingModel(positionList.Select(x=> x.MovementId).ToList());
            Control.ValidationEnd += Model.OnValidationFinish;

            Result = Model.Resultado;

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Memória: " + ((Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024), Core.Enums.ConsoleStatesEnum.WARNING);
        }

        public TrainingModel Model
        {
            get;
            set;
        }
        
        private ICommand m_changePosition;

        public ICommand ChangePosition
        {
            get { return m_changePosition ?? (m_changePosition = new DelegateCommand<object>(InternalChange)); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        private void InternalChange(object param)
        {
            TreinamentoEnum trainingType = Convert.ToString(param).Equals("Next") ? TreinamentoEnum.PROXIMA : TreinamentoEnum.REFAZER;

            var movimentCheck = (Control as ControlarTreinamento).IniciarValidacao(trainingType, Model.ButtonsChange);

            if (movimentCheck == MovimentosEnum.DISABLE)
                return;

            else if (trainingType == TreinamentoEnum.PROXIMA)
                Model.UpdateImage(movimentCheck);

            Model.ButtonsChange(0);
        }

        #region Dialogs

        /// <summary>
        /// 
        /// </summary>
        public override void RunPauseDialog()
        {
            Model.State = GameStateEnum.Paused;

            //caso a animação esteja em andamento não será necessário manipular os botões
            if (Control.Animation.State == StoryboardState.Stopped)
                Model.ButtonState(false);

            base.RunPauseDialog();
        }

        public override void ClosePauseDialog()
        {
            Model.State = GameStateEnum.Running;

            //caso a animação esteja em andamento não será necessário manipular os botões
            if (Control.Animation.State == StoryboardState.Stopped)
                Model.ButtonsChange(0);

            base.ClosePauseDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public override void RunDialog(object param = null)
        {
            Model.State = GameStateEnum.Running;

            if (Control.Animation.State == StoryboardState.Stopped)
                Model.ButtonState(false);

            base.RunDialog(param);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Return()
        {
            Control.ValidationEnd -= Model.OnValidationFinish;
            base.Return();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Redo()
        {
            Model.ButtonState(true);
            Model.ResetTraining();
            base.Redo();
        }

        #endregion
    }
}
