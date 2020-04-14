using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Models.Dialogs
{
    public class PositionDataTemplate
    {
        public PositionDataTemplate(string nome, int acertos, int erros, double desempenho)
        {
            Nome = nome;
            Acertos = acertos;
            Erros = erros;
            Desempenho = Convert.ToString(desempenho) + " %";
        }

        public string Nome
        {
            get;
            set;
        }

        public int Acertos
        {
            get;
            set;
        }

        public int Erros
        {
            get;
            set;
        }

        public string Desempenho
        {
            get;
            set;
        }
    }
}
