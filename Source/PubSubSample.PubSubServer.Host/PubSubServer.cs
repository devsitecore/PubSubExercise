// <copyright file="PubSubServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System.Windows.Forms;
    using Foundation.ServiceContracts;

    public partial class PubSubServer : Form, IPubSubServerHost
    {
        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubServer"/> class.
        /// </summary>
        /// <param name="pubSubServicesHostingManager">The PubSub Services Hosting Manager.</param>
        public PubSubServer(IPubSubServicesHostingManager pubSubServicesHostingManager)
        {
            this.InitializeComponent();
            pubSubServicesHostingManager.InitializeServicesHosting(this);
        }
        #endregion

        #region "Private Properties"

        /// <summary>
        /// Gets or sets the log text.
        /// </summary>
        /// <value>
        /// The log text.
        /// </value>
        private string LogText
        {
            get
            {
                return this.txtLog.Text;
            }

            set
            {
                this.txtLog.Text = value + "\r\n" + this.txtLog.Text;
            }
        }
        #endregion

        #region "IPubSubServerHost Implementation"
        public void Notify(string message)
        {
            this.LogText = message;
        }
        #endregion
    }
}