using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Dialogs
{
    public class DialogModel : ModelBase
    {
        public DialogModel(List<PositionDataTemplate> positionData)
        {
            DataCollection = new ObservableCollection<PositionDataTemplate>(positionData);
        }

        private ObservableCollection<PositionDataTemplate> m_dataCollection;

        public ObservableCollection<PositionDataTemplate> DataCollection
        {
            get { return m_dataCollection; }
            set { m_dataCollection = value; OnPropertyChanged("DataCollection"); }
        }
    }
}
