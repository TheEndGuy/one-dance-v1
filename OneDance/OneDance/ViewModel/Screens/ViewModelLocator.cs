using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using OneDance.Core.Enums;
using System;

namespace OneDance.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartMenuViewModel>();
            SimpleIoc.Default.Register<BodyStructureViewModel>();
            SimpleIoc.Default.Register<PositionsSelectViewModel>();
        }

        public MainViewModel MainMenu
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public StartMenuViewModel StartMenu
        {
            get { return ServiceLocator.Current.GetInstance<StartMenuViewModel>(); }
        }

        public BodyStructureViewModel BodyStructure
        {
            get { return ServiceLocator.Current.GetInstance<BodyStructureViewModel>(); }
        }

        public PositionsSelectViewModel PositionSelect
        {
            get { return ServiceLocator.Current.GetInstance<PositionsSelectViewModel>(); }
        }

        public static void Cleanup()
        {
        }
    }
}