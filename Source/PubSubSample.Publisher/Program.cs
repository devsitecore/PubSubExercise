// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Publisher
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
            var publisherForm = container.Resolve<Publisher>();
            Application.Run(publisherForm);
        }
    }
}