using Microsoft.Kinect;
using OneDance.Core.Database;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Registration
{
    public class UserModel
    {
        public UserModel()
        {
            Joints = new Dictionary<JointType, bool>()
            {
                [JointType.Head] = false,
                [JointType.ElbowRight] = false,
                [JointType.ElbowLeft] = false,
                [JointType.KneeRight] = false,
                [JointType.KneeLeft] = false,
            };
        }

        public Dictionary<JointType, bool> Joints
        {
            get;
            private set;
        }

        public List<PositionTemplate> UserPositions
        {
            get;
            set;
        } = new List<PositionTemplate>();

        /// <summary>
        /// Verifica se a posição existe na lista de posições do usuário
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public bool HasPosition(int positionId)
        {
            return UserPositions.Any(entry => entry.Id == positionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointType"></param>
        /// <param name="value"></param>
        public void AdicionarJoint(JointType jointType, bool @value)
        {
            Joints[jointType] = @value;
        }

        /// <summary>
        /// Adicionar uma nova posição na lista de posições do usuário, caso a mesma não exista
        /// </summary>
        /// <param name="position"></param>
        public void AdicionarPosicao(PositionTemplate position)
        {
            if (HasPosition(position.Id))
                return;

            UserPositions.Add(position);
        }

        /// <summary>
        /// Remove a posição da lista de posições do usuário, caso a mesma exista
        /// </summary>
        /// <param name="position"></param>
        public void RemoverPosicao(int positionId)
        {
            if (!HasPosition(positionId))
                return;

            UserPositions.Remove(UserPositions.FirstOrDefault(x=> x.Id == positionId));
        }
    }
}
