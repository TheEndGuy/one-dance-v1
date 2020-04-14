using OneDance.Core.Enums;
using System;
using System.ComponentModel;

namespace OneDance.Models
{
    public static class ConsoleLog
    {
        private static String text;

        public static String Text
        {
            get { return text; }

            set { text = value; NotifyStaticPropertyChanged("Text"); }
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static void WriteLog(string msgLog, ConsoleStatesEnum stateType)
        {
            Text += "\r\n" + "(" + Convert.ToString(stateType)  + ") " + msgLog;
        }
    }
}
