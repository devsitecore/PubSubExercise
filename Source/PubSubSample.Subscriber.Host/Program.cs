// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Subscriber
{
    using System;
    using System.Windows.Forms;
    using Common.Extensions;
    using Common.Unity;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var container = DependencyInjection.Instance().Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var subscriberForm = container.Resolve<Subscriber>();
            Application.Run(subscriberForm);
        }
    }
}