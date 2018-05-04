// <copyright file="PubSubServer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;
    using Common.Proxy;
    using Foundation.Contracts;

    public partial class PubSubServer : Form
    {
        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubServer"/> class.
        /// </summary>
        /// <param name="proxyManager">The proxy manager.</param>
        public PubSubServer(IProxyManager proxyManager)
        {
            this.ProxyManager = proxyManager;

            this.InitializeComponent();
            this.InitServiceHosting();
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

        /// <summary>
        /// Gets or sets the proxy manager.
        /// </summary>
        /// <value>
        /// The proxy manager.
        /// </value>
        private IProxyManager ProxyManager { get; set; }
        #endregion

        #region "Service Hosting"

        /// <summary>
        /// InitServiceHosting
        /// </summary>
        private void InitServiceHosting()
        {
            try
            {
                this.HostPublishService();
                this.HostSubscriptionService();
            }
            catch (Exception exp)
            {
                this.LogText = exp.Message;
            }
        }

        /// <summary>
        /// Hosts the subscription service.
        /// </summary>
        private void HostSubscriptionService()
        {
            var endPointAddress = ConfigurationManager.AppSettings["SubEndpointAddress"];
            this.ProxyManager.HostService<SubscriptionService, ISubscription>(endPointAddress);
            this.LogText = "Sub Service Hosted...";
        }

        /// <summary>
        /// Hosts the publish service.
        /// </summary>
        private void HostPublishService()
        {
            var endPointAddress = ConfigurationManager.AppSettings["PubEndpointAddress"];
            this.ProxyManager.HostService<PublishingService, IPublishing>(endPointAddress);
            this.LogText = "Pub Service Hosted...";
        }
        #endregion
    }
}