using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using OneDance.Core.Game.Main.Principal;
using OneDance.Core.Utilities;
using OneDance.Models;
using OneDance.Models.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OneDance.ViewModel
{
    public class GameViewModel : AbstractGameViewModel
    {
        public GameViewModel(List<PositionTemplate> positionList)
            : base(DialogTypeEnum.GAME, positionList)
        {
            Model = new GameModel(positionList.Select(x => x.MovementId).ToList());
            Control.ValidationEnd += FinishValidation;

            Result = Model.Resultado;
            PositionTask();

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Memória: " + ((Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024), Core.Enums.ConsoleStatesEnum.WARNING);
        }

        public GameModel Model
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartValidation()
        {
            MovimentosEnum result = (Control as ControlarPrincipal).IniciarValidacao();
            Model.UpdateImage(result);

            if (result == MovimentosEnum.DISABLE)
                RunDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="validationType"></param>
        public void FinishValidation(object sender, ValidacaoEnum validationType)
        {
            if (Model.Finalizado || Control.GameState == GameStateEnum.Finished)
                return;

            Model.OnValidationFinish(validationType);
            PositionTask();
        }

        private DispatcherOperation m_operation;

        /// <summary>
        /// 
        /// </summary>
        private async void PositionTask()
        {
            if (m_operation != null && m_operation.Status != DispatcherOperationStatus.Completed)
                m_operation.Abort();

            await Task.Delay(800);

            m_operation = Application.Current.Dispatcher.BeginInvoke(
                          new Action(() => StartValidation()),
                          DispatcherPriority.Background);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        private void Return(object param)
        {
            Control.ValidationEnd -= FinishValidation;
            base.Return();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Redo()
        {
            Model.ResetGame();
            base.Redo();

            PositionTask();
        }
    }
}
