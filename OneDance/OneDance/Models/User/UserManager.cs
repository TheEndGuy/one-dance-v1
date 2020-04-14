using Microsoft.Kinect;
using OneDance.Core.Database;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Models.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.User
{
    public class UserManager
    {
        private UserManager()
        {
            Model = new UserModel();
        }

        private static UserManager m_userManager;

        public static UserManager Instance
        {
            get { return m_userManager ?? (m_userManager = new UserManager()); }
        }

        public UserModel Model
        {
            get;
            set;
        }

        public PlayTypeEnum Type
        {
            get;
            set;
        }

        public void ResetUser()
        {
            Model.Joints.Keys.ToList().ForEach(x => Model.Joints[x] = false);
            Model.UserPositions = new List<PositionTemplate>();
        }

        /// <summary>
        /// Retorna verdadeiro se pelo menos um movimento foi selecionado
        /// </summary>
        /// <returns></returns>
        public bool HasMinPositions()
        {
            return Model.UserPositions.Count >= 1;
        }

        /// <summary>
        /// Retorna verdadeiro se pelo menos duas articulações foram selecionadas
        /// </summary>
        /// <returns></returns>
        public bool HasMinJoints()
        {
            return Model.Joints.Where(entry => entry.Value == true).ToList().Count >= 1;
        }

        /// <summary>
        /// Adiciona o valor de cada articulação do usuário (true/false)
        /// </summary>
        /// <param name="jointValues"></param>
        public void AdicionarJoints(Dictionary<JointType, bool> jointValues)
        {
            foreach (var @key in jointValues.Keys)
                Model.AdicionarJoint(@key, jointValues[@key]);
        }

        /// <summary>
        /// Método usado para gerenciar as posições selecionadas pelo usuário
        /// </summary>
        /// <param name="param">Param[1]: Adicionar(True) / Remover (False) -- Param[0]: Posição a ser adicionada/removida</param>
        public void GerenciarPosicoes(object[] param)
        {
            int positionId = Convert.ToInt32(param[0]);

            if (Convert.ToBoolean(param[1]))
                Model.AdicionarPosicao(ActivityManager.Instance.GetPosition((MovimentosEnum)positionId));

            else
                Model.RemoverPosicao(positionId);
        }

        /// <summary>
        /// Retorna as articulações que não foram selecionadas pelo usuário
        /// </summary>
        /// <returns></returns>
        public List<JointType> GetJoints()
        {
            return Model.Joints.Where(x => x.Value == false).Select(entry => entry.Key).ToList();
        }
    }
}
