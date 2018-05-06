// <copyright file="SubscriptionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System;
    using System.ServiceModel;
    using Core.Base;
    using Foundation.Contracts;
    using Foundation.DataContracts;

    /// <summary>
    /// SubscriptionService class
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SubscriptionService : BaseService, ISubscription
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionService"/> class.
        /// </summary>
        /// <param name="pubSubFilter">PubSubFilter</param>
        public SubscriptionService(IPubSubFilter pubSubFilter = null)
            : base(pubSubFilter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionService"/> class.
        /// </summary>
        public SubscriptionService()
        {
        }
        #endregion

        #region ISubscription Members

        /// <summary>
        /// Subscribe to a topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public void Subscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<ISubscription>();
            this.PubSubFilter.AddSubscriber(topic, subscriber);
        }

        /// <summary>
        /// UnSubscribe from the topic
        /// </summary>
        /// <param name="topic">Topic</param>
        public void UnSubscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<ISubscription>();
            this.PubSubFilter.RemoveSubscriber(topic, subscriber);
        }

        /// <summary>
        /// Receive the pub sub message
        /// </summary>
        /// <param name="pubSubMessage">Message</param>
        public void Receive(PubSubMessage pubSubMessage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}