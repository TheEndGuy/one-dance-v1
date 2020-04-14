using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Kinect;
using OneDance.Core.Enums;
using OneDance.Models.Registration;
using OneDance.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneDance.ViewModel
{
    public class BodyStructureViewModel : ViewModelBase, ILoad
    {
        public BodyStructureViewModel()
        {
            Model = new BodyStructureModel();
        }
        
        public BodyStructureModel Model
        {
            get;
            set;
        }

        public ICommand ChangeToStartMenu
        {
            get { return new RelayCommand(() => { Messenger.Default.Send(new SwitchViewMessage { ViewModel = ViewModelEnum.START_MENU }); }); }
        }

        private SnackbarMessageQueue m_customMessage;
        
        public SnackbarMessageQueue CustomMessage
        {
            get { return m_customMessage ?? (m_customMessage = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000))); }
        }
        
        private ICommand m_changeToPositionSelect;

        public ICommand ChangeToPositionsSelect
        {
            get { return m_changeToPositionSelect ?? (m_changeToPositionSelect = new DelegateCommand<object>(ChangePositionSelect)); }
        }

        /// <summary>
        /// Método para validação e alteração de cenário (Body -> Positions)
        /// </summary>
        /// <param name="param"></param>
        private void ChangePositionSelect(object param)
        {
            UserManager.Instance.AdicionarJoints(Model.CreateJointData());

            if (!UserManager.Instance.HasMinJoints())
            {
                CustomMessage.Enqueue("Por favor, selecione pelo menos uma parte corporal.", true);
                return;
            }

            Messenger.Default.Send(new SwitchViewMessage { ViewModel = ViewModelEnum.POSITION_SELECT });
        }

        public void Load()
        {
            Model.LoadJointData(UserManager.Instance.Model.Joints);
        }
    }
}
