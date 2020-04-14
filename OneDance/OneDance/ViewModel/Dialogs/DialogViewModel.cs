using OneDance.Models.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.ViewModel.Dialogs
{
    public class DialogViewModel
    {
        public DialogViewModel(List<PositionDataTemplate> positionData)
        {
            Model = new DialogModel(positionData);
        }

        public DialogModel Model
        {
            get;
            set;
        }
    }
}

