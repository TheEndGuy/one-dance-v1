using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Registration
{
    public class PositionsSelectModel : ModelBase
    {
        public PositionsSelectModel()
        {
            PositionCollection = new List<PositionTemplate>();
        }

        private List<PositionTemplate> positionCollection;

        public List<PositionTemplate> PositionCollection
        {
            get { return positionCollection; }
            set { positionCollection = value; OnPropertyChanged("PositionCollection"); }
        }
    }
}
