using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using OneDance.Core.Enums;
using OneDance.Models.Dialogs;
using OneDance.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneDance.ViewModel.Dialogs
{
    public class Dialog
    {
        /// <summary>
        /// Objeto DialogHost existente na TrainingView
        /// </summary>
        private string TrainingDialogHost = "TrainingDialog";

        /// <summary>
        /// Objeto DialogHost existente na GameView
        /// </summary>
        private string GameDialogHost = "GameDialog";

        public Dialog(DialogTypeEnum dialogType, Action redoAction, Action returnAction)
        {
            Type = dialogType;
            RedoAction = redoAction;
            ReturnAction = returnAction;
        }

        /// <summary>
        /// Ação usada para refazer o Game/Treinamento
        /// </summary>
        private Action RedoAction
        {
            get;
            set;
        }

        /// <summary>
        /// Ação usada para cancelar o Game/Treinamento e retornar ao menu principal
        /// </summary>
        private Action ReturnAction
        {
            get;
            set;
        }

        public UserControl View
        {
            get;
            set;
        }

        public DialogTypeEnum Type
        {
            get;
            set;
        }

        public async void Execute()
        {
            await DialogHost.Show(View, Type == DialogTypeEnum.TRAINING ? TrainingDialogHost : GameDialogHost, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            ExecuteResultAction(Convert.ToString(eventArgs.Parameter));
        }

        private void ExecuteResultAction(string buttomParameter)
        {
            if (buttomParameter.Equals("Repetir"))
                RedoAction?.Invoke();

            else if(buttomParameter.Equals("Retornar"))
                ReturnAction?.Invoke();
        }

        public void LoadContent(List<PositionDataTemplate> objectShow)
        {
            switch (Type)
            {
                case DialogTypeEnum.TRAINING:
                    View = new TreinamentoDialogView
                    {
                        DataContext = new DialogViewModel(objectShow)
                    };
                    break;

                case DialogTypeEnum.GAME:
                    View = new GameDialogView
                    {
                        DataContext = new DialogViewModel(objectShow)
                    };
                    break;

                default:
                    break;
            }
        }
    }
}
