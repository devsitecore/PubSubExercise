// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System;
    using System.Windows.Forms;
    using Common.Extensions;
    using Common.Unity;

    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = DependencyInjection.Instance().Initialize();
            var pubSubServer = container.Resolve<PubSubServer>();

            Application.Run(pubSubServer);
        }
    }
}