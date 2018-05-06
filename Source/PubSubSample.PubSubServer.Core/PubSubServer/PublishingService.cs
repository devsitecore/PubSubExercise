// <copyright file="PublishingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System.ServiceModel;
    using Core.Base;
    using Foundation.Contracts;
    using Foundation.DataContracts;

    /// <summary>
    /// Publishing
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PublishingService : BaseService, IPublishing
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingService"/> class.
        /// </summary>
        /// <param name="pubSubFilter">PubSubFilter</param>
        public PublishingService(IPubSubFilter pubSubFilter = null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingService"/> class.
        /// </summary>
        public PublishingService()
        {
        }
        #endregion

        #region IPublishing Members

        /// <summary>
        /// Publish Message for a topic
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="topic">Topic</param>
        public void Publish(PubSubMessage message, string topic)
        {
            this.PubSubFilter.Publish(message, topic);
        }
        #endregion
    }
}
