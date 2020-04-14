using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneDance.Core.Enums;
using OneDance.Core.Game.Treinamento;
using OneDance.Models.User;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OneDance.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase m_currentViewModel;

        public MainViewModel()
        {
            Messenger.Default.Register<SwitchViewMessage>(this, (switchViewMessage) =>
            {
                SwitchView(switchViewMessage.ViewModel);
            });

            Messenger.Default.Register<GameplayMessage>(this, (gameplayMessage) =>
            {
                StartGameplay();
            });

            SwitchView(ViewModelEnum.START_MENU);
        }

        public ViewModelBase CurrentViewModel
        {
            get { return m_currentViewModel; }
            set
            {
                if (m_currentViewModel == value)
                    return;

                m_currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public void StartGameplay()
        {
            SwitchView(UserManager.Instance.Type == PlayTypeEnum.GAME ? ViewModelEnum.GAME : ViewModelEnum.TRAINING);
        }

        public void SwitchView(ViewModelEnum viewModel)
        {
            if (viewModel == ViewModelEnum.START_MENU)
                CurrentViewModel = ServiceLocator.Current.GetInstance<StartMenuViewModel>();

            else if (viewModel == ViewModelEnum.BODY_STRUCTURE)
                CurrentViewModel = ServiceLocator.Current.GetInstance<BodyStructureViewModel>();

            else if (viewModel == ViewModelEnum.POSITION_SELECT)
                CurrentViewModel = ServiceLocator.Current.GetInstance<PositionsSelectViewModel>();

            else if (viewModel == ViewModelEnum.TRAINING)
                CurrentViewModel = new TrainingViewModel(UserManager.Instance.Model.UserPositions);

            else if (viewModel == ViewModelEnum.GAME)
                CurrentViewModel = new GameViewModel(UserManager.Instance.Model.UserPositions);

            if (CurrentViewModel != null && CurrentViewModel is ILoad)
                (CurrentViewModel as ILoad).Load();
        }
    }
}