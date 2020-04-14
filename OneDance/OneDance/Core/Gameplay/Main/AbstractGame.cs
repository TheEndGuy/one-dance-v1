using Microsoft.Kinect;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using OneDance.Core.Game.Movimentos;
using OneDance.Core.Game.Treinamento;
using OneDance.Core.Utilities;
using OneDance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDance.Core.Game.Main
{
    public abstract class AbstractGame
    {
        public AbstractGame(List<PositionTemplate> posicoesValidacao)
        {
            Validacoes = new List<Validacao>();
            Validacoes = posicoesValidacao.Select(x => new Validacao(x)).ToList();
            CriarRastreadores();
        }

        public GameStateEnum State
        {
            get;
            set;
        }

        public List<Validacao> Validacoes
        {
            get;
            set;
        }

        public List<ITracker> Rastreadores
        {
            get;
            set;
        }
        
        public abstract int CurrentIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Cria um novo rastreador para cada posição a ser validada, atribuindo o evento de identificação
        /// </summary>
        private void CriarRastreadores()
        {
            Rastreadores = new List<ITracker>();

            foreach (var validacao in Validacoes)
            {
                var rastreadorMovimento = validacao.CriarRastreador();

                rastreadorMovimento.MovimentoIdentificado += OnPositionIdentified;
                Rastreadores.Add(rastreadorMovimento);
            }
        }

        /// <summary>
        /// Remove os eventos de identificação de posição de cada rastreador
        /// </summary>
        public void RemoverRastreadores()
        {
            foreach (var rastreador in Rastreadores)
                (rastreador as Tracker).MovimentoIdentificado -= OnPositionIdentified;
        }

        /// <summary>
        /// Inicia a validação de um movimento
        /// </summary>
        public virtual void Start()
        {
            Validacoes[CurrentIndex].IniciarValidacao();

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Validação do movimento " + Validacoes[CurrentIndex].Movimento + " iniciada.", ConsoleStatesEnum.NOTICE);
        }

        /// <summary>
        /// Finaliza a validação de um movimento
        /// </summary>
        public virtual void Finish()
        {
            Validacoes[CurrentIndex].FinalizarValidacao();

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Validação do movimento " + Validacoes[CurrentIndex].Movimento + " finalizada.", ConsoleStatesEnum.NOTICE);
        }

        /// <summary>
        /// Evento usado para rastrear os movimentos frequentemente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTracker(object sender, AllFramesReadyEventArgs e)
        {
            var skeletonFrame = e.OpenSkeletonFrame();

            using (skeletonFrame)
            {
                Util.Rastrear(skeletonFrame, Rastreadores.ToArray());
            }
        }

        /// <summary>
        /// Evento realizado após identificar um movimento no rastreador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="validationEvent"></param>
        public abstract void OnPositionIdentified(object sender, EventArgs validationEvent);

        /// <summary>
        /// Checagem se é possível chamar o próximo movimento
        /// </summary>
        /// <returns></returns>
        public abstract bool CanNext();
    }
}
