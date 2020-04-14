using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OneDance.Core.Enums;
using OneDance.Models.User;
using System;
using System.Windows.Input;

namespace OneDance.ViewModel
{
    public class StartMenuViewModel : ViewModelBase, ILoad
    {
        public StartMenuViewModel()
        {
        }
        
        private ICommand m_changeBodyStructureView;

        public ICommand ChangeBodyStructureView
        {
            get { return m_changeBodyStructureView ?? (m_changeBodyStructureView = new DelegateCommand<object>(ChangeBodyStructure)); }
        }

        private void ChangeBodyStructure(object param)
        {
            UserManager.Instance.Type = (PlayTypeEnum)Convert.ToInt32(param);

            Messenger.Default.Send(new SwitchViewMessage { ViewModel = ViewModelEnum.BODY_STRUCTURE });
        }

        public void Load()
        {
            UserManager.Instance.ResetUser();
        }
    }
}
