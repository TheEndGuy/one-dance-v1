using System;
using System.Collections.Generic;
using OneDance.Core.Enums;
using OneDance.Core.Database.Templates;
using OneDance.Views;
using OneDance.Core.Game.Manager.Animation;
using OneDance.Core.Gameplay.Manager.Animation;
using System.Windows.Media.Animation;

namespace OneDance.Core.Game.Main.Principal
{
    public class ControlarPrincipal : AbstractControl
    {
        private PrincipalGame GameClass
        {
            get { return (Game as PrincipalGame); }
        }

        private GameAnimation m_gameAnimation;

        public override GameAnimation Animation
        {
            get { return m_gameAnimation ?? (m_gameAnimation = new GameAnimation(GameView.GetStoryboard("EnteringPosition") as Storyboard, GameView.GetStoryboard("FinishAnimation") as Storyboard, HabilitarValidacao, DesabilitarValidacao)); }
            set { m_gameAnimation = value; }
        }

        private FeedbackAnimation m_feedbackAnimation;

        public override FeedbackAnimation FeedbackAnim
        {
            get { return m_feedbackAnimation ?? (m_feedbackAnimation = new FeedbackAnimation(GameView.GetStoryboard("FeedbackAcerto") as Storyboard, GameView.GetStoryboard("FeedbackErro") as Storyboard)); }
            set { m_feedbackAnimation = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override void IniciarJogo(List<PositionTemplate> userPositions)
        {
            Game = new PrincipalGame(userPositions);
            GameClass.PositionIdentified = DesabilitarValidacao;

            base.IniciarJogo(userPositions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MovimentosEnum IniciarValidacao()
        {
            MovimentosEnum movimento = GameClass.NextPositionValidation();

            if (movimento != MovimentosEnum.DISABLE)
            {
                Animation?.Start();

                if (GameState == Enums.Gameplay.GameStateEnum.Paused)
                    Animation?.Pause();
            }

            return movimento;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ResetarJogo()
        {
            GameClass.ResetGame();
            base.ResetarJogo();
        }
    }
}
