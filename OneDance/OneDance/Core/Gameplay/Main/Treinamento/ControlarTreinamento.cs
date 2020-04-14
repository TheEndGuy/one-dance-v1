using OneDance.Core.Connection.KinectStart;
using OneDance.Core.Database.Templates;
using OneDance.Core.Enums;
using OneDance.Core.Game.Main;
using OneDance.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using OneDance.Core.Game.Manager.Animation;
using OneDance.Core.Gameplay.Manager.Animation;
using System.Windows.Media.Animation;

namespace OneDance.Core.Game.Treinamento
{
    public class ControlarTreinamento : AbstractControl
    {
        private TreinamentoGame TreinamentoGame
        {
            get { return (Game as TreinamentoGame); }
        }

        private GameAnimation m_gameAnimation;

        public override GameAnimation Animation
        {
            get { return m_gameAnimation ?? (m_gameAnimation = new GameAnimation(TrainingView.GetStoryboard("EnteringPosition") as Storyboard, TrainingView.GetStoryboard("FinishAnimation") as Storyboard, HabilitarValidacao, DesabilitarValidacao)); }
            set { m_gameAnimation = value; }
        }

        private FeedbackAnimation m_feedbackAnimation;

        public override FeedbackAnimation FeedbackAnim
        {
            get { return m_feedbackAnimation ?? (m_feedbackAnimation = new FeedbackAnimation(TrainingView.GetStoryboard("FeedbackAcerto") as Storyboard, TrainingView.GetStoryboard("FeedbackErro") as Storyboard)); }
            set { m_feedbackAnimation = value; }
        }

        public MovimentosEnum IniciarValidacao(TreinamentoEnum trainingType, Action<int> buttonAction)
        {
            TreinamentoGame.TipoTreinamento = trainingType;

            MovimentosEnum movimento = TreinamentoGame.GetPosition();
            
            if (!CanPlay || movimento != MovimentosEnum.DISABLE)
                Animation?.Start(buttonAction);

            return movimento;
        }
        
        public override void IniciarJogo(List<PositionTemplate> userPositions)
        {
            Game = new TreinamentoGame(userPositions);
            TreinamentoGame.PositionIdentified = DesabilitarValidacao;

            base.IniciarJogo(userPositions);
        }

        public override void HabilitarValidacao()
        {
            if (TreinamentoGame.Enabled)
                return;

            else
                TreinamentoGame.Enabled = true;

            base.HabilitarValidacao();
        }

        public override void DesabilitarValidacao(ValidacaoEnum validationType)
        {
            if (!TreinamentoGame.Enabled)
                return;

            base.DesabilitarValidacao(validationType);
        }
    }
}

