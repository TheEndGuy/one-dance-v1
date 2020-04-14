using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Registration
{
    public class BodyStructureModel : ModelBase
    {
        public BodyStructureModel()
        { 
        }

        private bool head;

        public bool Head
        {
            get { return head; }
            set { head = value; OnPropertyChanged("Head"); }
        }

        private bool rightArm;

        public bool RightArm
        {
            get { return rightArm; }
            set { rightArm = value; OnPropertyChanged("RightArm"); }
        }

        private bool leftArm;

        public bool LeftArm
        {
            get { return leftArm; }
            set { leftArm = value; OnPropertyChanged("LeftArm"); }
        }

        private bool rightLeg;

        public bool RightLeg
        {
            get { return rightLeg; }
            set { rightLeg = value; OnPropertyChanged("RightLeg"); }
        }

        private bool leftLeg;

        public bool LeftLeg
        {
            get { return leftLeg; }
            set { leftLeg = value; OnPropertyChanged("LeftLeg"); }
        }

        /// <summary>
        /// Retorna um dicionário contendo os dados de cada posição escolhida pelo usuário
        /// </summary>
        /// <returns></returns>
        public Dictionary<JointType, bool> CreateJointData()
        {
            return new Dictionary<JointType, bool>()
            {
                [JointType.Head] = head,
                [JointType.ElbowRight] = rightArm,
                [JointType.ElbowLeft] = leftArm,
                [JointType.KneeRight] = rightLeg,
                [JointType.KneeLeft] = leftLeg,
            };
        }

        /// <summary>
        /// Carrega os valores já definidos para cada articulação
        /// </summary>
        /// <param name="jointData"></param>
        public void LoadJointData(Dictionary<JointType, bool> jointData)
        {
            head = jointData[JointType.Head];
            rightArm = jointData[JointType.ElbowRight];
            leftArm = jointData[JointType.ElbowLeft];
            rightLeg = jointData[JointType.KneeRight];
            leftLeg = jointData[JointType.KneeLeft];
        }
    }
}
