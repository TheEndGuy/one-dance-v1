using System;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using OneDance.Models;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using OneDance.ViewModel;
using System.Diagnostics;

namespace OneDance.Core.Connection.KinectStart
{
    public class KinectStart
    {
        private KinectStart()
        {
        }

        private static KinectStart m_kinectStart;

        public static KinectStart Instance
        {
            get { return m_kinectStart ?? (m_kinectStart = new KinectStart()); }
        }

        private KinectSensorChooser m_kinectChooser;

        public KinectSensorChooser KinectSelector
        {
            get { return m_kinectChooser ?? (m_kinectChooser = new KinectSensorChooser()); }
            private set { m_kinectChooser = value; }
        }

        private Action<KinectSensor> KinectSensorStarted
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return KinectSelector.Kinect != null && KinectSelector.Status == ChooserStatus.SensorStarted; }
        }

        public KinectSensor Kinect
        {
            get { return KinectSelector.Kinect; }
        }
        
        /// <summary>
        /// Start a new KinectSensor and your events
        /// </summary>
        /// <param name="elevationAngle"></param>
        /// <returns>The KinectSensor</returns>
        public KinectSensor StartKinect()
        {
            KinectSelector.KinectChanged += OnKinectStatsChange;
            KinectSelector.Start();

            if (KinectSelector.Kinect != null)
            {
                OnKinectStarted();

                if(Configuration.DEBUG_ON)
                    ConsoleLog.WriteLog("Kinect habilitado.", Enums.ConsoleStatesEnum.NOTICE);
            }

            else if(Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Erro ao inicializar o Kinect.", Core.Enums.ConsoleStatesEnum.WARNING);

            return KinectSelector.Kinect;
        }

        /// <summary>
        /// Disable the KinectSensor
        /// </summary>
        public void DisableKinect()
        {
            var sensorKinect = KinectSelector.Kinect;

            KinectSelector.KinectChanged -= OnKinectStatsChange;
            KinectSelector.Stop();

            if (sensorKinect == null)
                return;

            sensorKinect.Stop();

            if (sensorKinect.SkeletonStream.IsEnabled)
                sensorKinect.SkeletonStream.Disable();

            if (sensorKinect.ColorStream.IsEnabled)
                sensorKinect.ColorStream.Disable();

            if (sensorKinect.DepthStream.IsEnabled)
                sensorKinect.DepthStream.Disable();

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("Kinect desabilitado.", Enums.ConsoleStatesEnum.NOTICE);
        }

        /// <summary>
        /// Enable Kinect Streams and functions when connect
        /// </summary>
        /// <param name="kinect"></param>
        private void OnKinectStarted()
        {
            Kinect.DepthStream.Enable();
            Kinect.SkeletonStream.Enable();
            Kinect.ColorStream.Enable();

            if (Configuration.DEBUG_ON)
                ConsoleLog.WriteLog("KinectSensor Streams Habilitados.", Core.Enums.ConsoleStatesEnum.NOTICE);
        }

        /// <summary>
        /// Event to enable/disable Kinect sensors when connecting or disconnecting from equipment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="kinectArgs"></param>
        private void OnKinectStatsChange(object sender, KinectChangedEventArgs kinectArgs)
        {
            if (kinectArgs.OldSensor != null)
            {
                try
                {
                    if (Configuration.DEBUG_ON)
                        ConsoleLog.WriteLog("Desabilitando o KinectSensor.", Enums.ConsoleStatesEnum.NOTICE);

                    if (kinectArgs.OldSensor.DepthStream.IsEnabled)
                        kinectArgs.OldSensor.DepthStream.Disable();

                    if (kinectArgs.OldSensor.SkeletonStream.IsEnabled)
                        kinectArgs.OldSensor.SkeletonStream.Disable();

                    if (kinectArgs.OldSensor.ColorStream.IsEnabled)
                        kinectArgs.OldSensor.ColorStream.Disable();
                }

                catch (InvalidOperationException)
                {
                    if (Configuration.DEBUG_ON)
                        ConsoleLog.WriteLog("Ocorreu um erro ao desabilitar o KinectSensor.", Enums.ConsoleStatesEnum.WARNING);
                }
            }

            if (kinectArgs.NewSensor != null)
                KinectSensorStarted?.Invoke(KinectSelector.Kinect);
        }
    }
}
