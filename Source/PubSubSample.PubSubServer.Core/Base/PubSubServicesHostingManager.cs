// <copyright file="PubSubServicesHostingManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.Base
{
    using System;
    using System.Configuration;
    using Contracts;
    using PubSubServer;
    using ServiceContracts;

    /// <summary>
    /// PubSubServicesHostingManager Class
    /// </summary>
    public class PubSubServicesHostingManager : IPubSubServicesHostingManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubServicesHostingManager"/> class.
        /// </summary>
        /// <param name="proxyManager">The proxy manager</param>
        public PubSubServicesHostingManager(IProxyManager proxyManager)
        {
            this.ProxyManager = proxyManager;
        }

        /// <summary>
        /// Gets or sets the proxy manager
        /// </summary>
        protected IProxyManager ProxyManager { get; set; }

        /// <summary>
        /// Initialize Services Hosting
        /// </summary>
        /// <param name="host">The host</param>
        /// <returns>True if initialization is good</returns>
        public virtual bool InitializeServicesHosting(IPubSubServerHost host)
        {
            var initialized = false;

            try
            {
                this.ProxyManager.Initialize(host);
                this.HostPublishService();
                this.HostSubscriptionService();

                initialized = true;
            }
            catch (Exception exp)
            {
                this.ProxyManager.NotifyHost(exp.Message);
            }

            return initialized;
        }

        /// <summary>
        /// Hosts the subscription service.
        /// </summary>
        protected virtual void HostSubscriptionService()
        {
            var endPointAddress = ConfigurationManager.AppSettings["SubEndpointAddress"];
            this.ProxyManager.HostService<SubscriptionService, ISubscription>(endPointAddress);
            this.ProxyManager.NotifyHost("Subscribers Service Hosted...");
        }

        /// <summary>
        /// Hosts the publish service.
        /// </summary>
        protected virtual void HostPublishService()
        {
            var endPointAddress = ConfigurationManager.AppSettings["PubEndpointAddress"];
            this.ProxyManager.HostService<PublishingService, IPublishing>(endPointAddress);
            this.ProxyManager.NotifyHost("Publishers Service Hosted...");
        }
    }
}
