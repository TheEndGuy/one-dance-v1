using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using OneDance.Core.Database;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Models.Registration;
using OneDance.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OneDance.ViewModel
{
    public class PositionsSelectViewModel : ViewModelBase, ILoad
    {
        public PositionsSelectViewModel()
        {
            Model = new PositionsSelectModel();
        }

        public PositionsSelectModel Model
        {
            get;
            set;
        }

        private SnackbarMessageQueue m_customMessage;

        public SnackbarMessageQueue CustomMessage
        {
            get { return m_customMessage ?? (m_customMessage = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))); }
        }

        private ICommand m_positionManager;

        public ICommand PositionsManager
        {
            get { return m_positionManager ?? (m_positionManager = new DelegateCommand<object[]>(UserManager.Instance.GerenciarPosicoes)); }
        }

        private ICommand m_changeToGameplay;

        public ICommand ChangeToGameplay
        {
            get { return m_changeToGameplay ?? (m_changeToGameplay = new DelegateCommand<object>(StartGameplay)); }
        }

        public ICommand ChangeToBodyStructure
        {
            get { return new RelayCommand(() => { Messenger.Default.Send(new SwitchViewMessage { ViewModel = ViewModelEnum.BODY_STRUCTURE } ); }); }
        }

        private void StartGameplay(object param)
        {
            if(!UserManager.Instance.HasMinPositions())
            {
                CustomMessage.Enqueue("Por favor, selecione pelo menos um movimento.", true);
                return;
            }

            Messenger.Default.Send(new GameplayMessage());
        }

        public void Load()
        {
            Model.PositionCollection = new List<PositionTemplate>(ActivityManager.Instance.FilterList(UserManager.Instance.GetJoints()));
        }
    }
}


