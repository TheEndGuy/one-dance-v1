using OneDance.Core.Database;
using OneDance.Core.Enums;
using OneDance.Models.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models
{
    public class PositionResult
    {
        public PositionResult(List<MovimentosEnum> positionList)
        {
            Total = new Dictionary<MovimentosEnum, int>(positionList.ToDictionary(x => x, x => 0));
            TotalAcertos = new Dictionary<MovimentosEnum, int>(positionList.ToDictionary(x => x, x => 0));
        }

        public Dictionary<MovimentosEnum, int> TotalAcertos
        {
            get;
            set;
        }

        public Dictionary<MovimentosEnum, int> Total
        {
            get;
            set;
        }

        public bool PrimeiraValidacao
        {
            get { return Total.Values.All(x => x == 0); }
        }
        
        /// <summary>
        /// Adiciona uma nova tentativa no total de tentativas, e em acertos caso for um movimento válido
        /// </summary>
        /// <param name="validationType"></param>
        public void AdicionarTotal(ValidacaoEnum validationType, MovimentosEnum currentMovement)
        {
            Total[currentMovement]++;

            if (validationType == ValidacaoEnum.VALIDO)
                TotalAcertos[currentMovement]++;
        }

        /// <summary>
        /// Retorna a média (TotalAcertos/Total) de um determinado movimento
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public double CalcularMedia(MovimentosEnum position)
        {
            return (Math.Round((double)(((double)TotalAcertos[position]) / ((double)Total[position])), 2) * 100);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<PositionDataTemplate> CreateData()
        {
            if (PrimeiraValidacao)
                return new List<PositionDataTemplate>();

            List<PositionDataTemplate> m_positionData = new List<PositionDataTemplate>();

            foreach (var @dataValue in Total)
            {
                if (Total[@dataValue.Key] == 0)
                    continue;

                PositionDataTemplate dataTemplate = new PositionDataTemplate(
                                                     ActivityManager.Instance.GetPosition(@dataValue.Key).Model.Nome, //Nome
                                                     TotalAcertos[@dataValue.Key], //Acertos
                                                     (@dataValue.Value - TotalAcertos[@dataValue.Key]), //Erros
                                                     CalcularMedia(@dataValue.Key)); // Desempenho

                m_positionData.Add(dataTemplate);
            }

            return m_positionData;
        }

        /// <summary>
        /// Reseta as atribuições de resultado
        /// </summary>
        public void Reset()
        {
            Total.Keys.ToList().ForEach(entry => 
            {
                Total[entry] = 0;
                TotalAcertos[entry] = 0;
            });
        }
    }
}
