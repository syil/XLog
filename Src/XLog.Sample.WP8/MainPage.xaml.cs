﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using XLog.Formatters;
using System.Diagnostics;
using XLog.ConsoleApp;

namespace XLog.Sample.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private LogConfig logConfig;

        public MainPage()
        {
            InitializeComponent();

            var formatter = new LineFormatter();
            logConfig = new LogConfig(formatter);

            logConfig.AddTarget(LogLevel.Trace, LogLevel.Fatal, new SyncFileTarget("Logs", "Log"));
            
            Button.Click += OnButtonClick;
        }
                
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            LogManager.Init(logConfig);

            var logger = LogManager.Default.GetLogger("TestLogger");

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000; i++)
            {
                logger.Debug("Hello {0}", i);
            }
            LogManager.Default.Flush();
            sw.Stop();
            MessageBox.Show(sw.Elapsed.ToString());
        }

        
    }
}