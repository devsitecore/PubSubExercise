// <copyright file="PublishingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System.ServiceModel;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.DataContracts;

    /// <summary>
    /// Publishing
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PublishingService : IPublishing
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingService"/> class.
        /// </summary>
        public PublishingService()
        {
            this.PubSubFilter = DependencyInjection.Instance().Container.Resolve<IPubSubFilter>();
        }
        #endregion

        #region Private Properties
        private IPubSubFilter PubSubFilter { get; set; }
        #endregion

        #region IPublishing Members
        public void Publish(PubSubMessage e, string topic)
        {
            this.PubSubFilter.Publish(e, topic);
        }
        #endregion
    }
}
