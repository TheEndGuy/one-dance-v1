using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Enums.Gameplay;
using OneDance.Core.Game.Manager;
using OneDance.Core.Game.Manager.Animation;
using OneDance.Core.Game.Movimentos;
using OneDance.Core.Gameplay.Manager.Animation;
using OneDance.Core.Gameplay.Music;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneDance.Core.Game.Main
{
    public abstract class AbstractControl
    {
        public event EventHandler<ValidacaoEnum> ValidationEnd;

        public AbstractControl()
        {
            Central = new GameCore();

            //Primeira versão do controle de músicas
            GameSong = new MediaControl(new List<Database.Models.MusicModel>()
            {
                new Database.Models.MusicModel(@"\Songs\shake_it_off.mp3", "Shake It Off"),
                new Database.Models.MusicModel(@"\Songs\echame_la_culpa.mp3", "Echame La Culpa")
            });
        }

        public GameCore Central
        {
            get;
            set;
        }

        public AbstractGame Game
        {
            get;
            set;
        }

        public abstract GameAnimation Animation
        {
            get;
            set;
        }
        
        public abstract FeedbackAnimation FeedbackAnim
        {
            get;
            set;
        }

        public MediaControl GameSong
        {
            get;
            set;
        }

        public GameStateEnum GameState
        {
            get { return Game.State; }
        }

        public bool CanPlay
        {
            get { return Central.Sensor != null; }
        }
        
        /// <summary>
        /// Finaliza as referêncais removendo os objetos linkados da memória
        /// </summary>
        public bool Finalizar()
        {
            Game.State = GameStateEnum.Finished;
            Central.Dispose();

            DesabilitarValidacao(ValidacaoEnum.FINALIZADO);
            Game.RemoverRastreadores();

            Animation?.Finish();

            //Teste
            GameSong?.Finalizar();

            return true;
        }

        /// <summary>
        /// Habilita validação
        /// </summary>
        public virtual void HabilitarValidacao()
        {
            foreach (var tracker in Game.Rastreadores.Where(x => ((x as Tracker).Movimento is Gesto)).ToList())
                (tracker as Tracker).ReiniciarGesto();

            Central.GameFrames += Game.OnTracker;
            Game.Start();
        }

        /// <summary>
        /// Desabilita a validação
        /// </summary>
        public virtual void DesabilitarValidacao(ValidacaoEnum validationType)
        {
            Central.GameFrames -= Game.OnTracker;

            Game.Finish();
            ValidationEnd?.Invoke(null, validationType);

            Animation?.FinishAnimation();

            if (validationType == ValidacaoEnum.FINALIZADO)
                return;

            FeedbackAnim.Execute(validationType);
        }

        public void PausarJogo()
        {
            Game.State = GameStateEnum.Paused;

            //Teste
            GameSong.Pause();

            if (Animation == null)
                return;

            Animation?.Pause();
        }

        public void ResumirJogo()
        {
            Game.State = GameStateEnum.Running;

            //Teste
            GameSong.Start();

            if (Animation == null)
                return;

            Animation?.Resume();
        }

        /// <summary>
        /// Reseta o Game
        /// </summary>
        public virtual void ResetarJogo()
        {
            Game.CurrentIndex = 0;

            foreach (var val in Game.Validacoes)
                val.IniciarValidacao();

            if (Animation.State == StoryboardState.Paused)
            {
                Animation?.Finish();
            }

            //Teste
            GameSong.Randomize();
        }

        /// <summary>
        /// Inicia o Game
        /// </summary>
        public virtual void IniciarJogo(List<PositionTemplate> userPositions)
        {
            Game.State = GameStateEnum.Running;

            //Teste
            GameSong.Start();
        }
    }
}

