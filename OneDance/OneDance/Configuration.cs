using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance
{
    public static class Configuration
    {
        /// <summary>
        /// Ativa os recursos do modo Debug
        /// </summary>
        public static bool DEBUG_ON = true;

        /// <summary>
        /// Tempo mínimo para validação da pose (Segundos)
        /// </summary>
        public static int MIN_VALID_TIME = 3;

        /// <summary>
        /// Tempo máximo para validação da pose (Segundos)
        /// </summary>
        public static int MAX_VALID_TIME = 7;

        /// <summary>
        /// Máximo de validações por movimento em um Jogo
        /// </summary>
        public static int GAME_VALIDATIONS = 4;

        /// <summary>
        /// Distância mínima para o reconhecimento corporal de um jogador
        /// </summary>
        public static double DISTANCE_CHECK = 2;

        /// <summary>
        /// Arquivo de configuração do banco de dados
        /// </summary>
        public static string CONFIGURATION_FILE = @"\DatabaseConfiguration.json";
    }
}
