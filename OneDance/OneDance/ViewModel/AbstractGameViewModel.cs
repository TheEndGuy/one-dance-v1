using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using OneDance.Core.Game.Main;
using OneDance.Core.Game.Main.Principal;
using OneDance.Core.Game.Treinamento;
using OneDance.Models;
using OneDance.ViewModel.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

namespace OneDance.ViewModel
{
    public abstract class AbstractGameViewModel : ViewModelBase
    {
        public AbstractGameViewModel(DialogTypeEnum dialogType, List<PositionTemplate> positionList)
        {
            InternalDialog = new Dialog(dialogType, Redo, Return);
            PauseDialog = new PauseDialog(RunDialog);

            CreateControl();

            Control.Central.FrameChanged += FrameChanged;
            Control.IniciarJogo(positionList);
        }

        public AbstractControl Control
        {
            get;
            set;
        }

        private ImageBrush m_image;

        public ImageBrush Image
        {
            get { return m_image; }
            set
            {
                m_image = value;
                RaisePropertyChanged("Image");
            }
        }

        private Dialog InternalDialog
        {
            get;
            set;
        }

        private PauseDialog PauseDialog
        {
            get;
            set;
        }

        public PositionResult Result
        {
            get;
            set;
        }

        private void FrameChanged(ImageBrush image, bool hasPlayer)
        {
            Image = image;

            if (Control.GameState == GameStateEnum.OnDialog)
                return;

            else if (Control.GameState == GameStateEnum.Paused && hasPlayer)
            {
                Control.ResumirJogo();
                ClosePauseDialog();
                return;
            }

            else if (Control.GameState == GameStateEnum.Running && !hasPlayer)
            {
                RunPauseDialog();
            }
        }

        private void CreateControl()
        {
            if (InternalDialog.Type == DialogTypeEnum.TRAINING)
                Control = new ControlarTreinamento();

            else
                Control = new ControlarPrincipal();
        }

        #region Dialogs

        #region Pause

        public virtual void RunPauseDialog()
        {
            if (Control.GameState == GameStateEnum.OnDialog)
                return;
            
            Control.PausarJogo();
            PauseDialog.Execute();
        }

        public virtual void ClosePauseDialog()
        {
            if (Control.GameState == GameStateEnum.OnDialog)
                Control.Game.State = GameStateEnum.Running;

            PauseDialog.Close();
        }

        #endregion

        #region End

        public virtual void RunDialog(object param = null)
        {
            //caso finalize, forçamos a exibição do dialog do resultado
            if (Control.GameState == GameStateEnum.Paused)
                PauseDialog.Close();

            if (Control.GameState == GameStateEnum.OnDialog)
                return;

            else 
                Control.Game.State = GameStateEnum.OnDialog;

            //Teste
            Control.GameSong.Stop();

            InternalDialog.LoadContent(Result.CreateData());
            InternalDialog.Execute();
        }

        public virtual void Return()
        {
            Control.Central.FrameChanged -= FrameChanged;

            if (Control.Finalizar())
            {
                Messenger.Default.Send(new SwitchViewMessage { ViewModel = ViewModelEnum.START_MENU });

                if (Configuration.DEBUG_ON)
                    ConsoleLog.WriteLog("Memória: " + ((Process.GetCurrentProcess().PrivateMemorySize64 / 1024) / 1024), Core.Enums.ConsoleStatesEnum.WARNING);
            }
        }

        public virtual void Redo()
        {
            Control.ResetarJogo();

            if (Control.GameState == GameStateEnum.OnDialog)
                Control.Game.State = GameStateEnum.Running;
        }

        #endregion

        #endregion
    }
}
