// <copyright file="PubSubFilter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System.Collections.Generic;
    using Foundation.Contracts;
    using Foundation.DataContracts;
    using Foundation.ServiceContracts;

    /// <summary>
    /// PubSubFilter
    /// </summary>
    public class PubSubFilter : IPubSubFilter
    {
        #region "Private Members"
        private readonly IProxyManager proxyManager;
        private Dictionary<string, List<ISubscription>> subscribersList = new Dictionary<string, List<ISubscription>>();
        #endregion

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="PubSubFilter"/> class.
        /// </summary>
        /// <param name="proxyManager">Proxy Manager</param>
        public PubSubFilter(IProxyManager proxyManager)
        {
            this.proxyManager = proxyManager;
        }
        #endregion

        #region "Private Properties"

        /// <summary>
        /// Gets the subscribers list.
        /// </summary>
        /// <value>
        /// The subscribers list.
        /// </value>
        private Dictionary<string, List<ISubscription>> SubscribersList
        {
            get
            {
                lock (typeof(PubSubFilter))
                {
                    return this.subscribersList;
                }
            }
        }
        #endregion

        #region "IPubSubFilter Implementation"

        /// <summary>
        /// Publishes the specified e.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="topic">The topic.</param>
        public void Publish(PubSubMessage message, string topic)
        {
            this.proxyManager.NotifyHost(string.Format("New message is published for the topic {0}.", topic));

            var subscribers = this.GetSubscribers(topic);

            if (subscribers != null)
            {
                foreach (var subscriber in subscribers)
                {
                    try
                    {
                        subscriber.Receive(message);
                    }
                    catch
                    {
                        // This normally happens when the subscriber is closed without un-subscribing
                    }
                }
            }
        }

        /// <summary>
        /// Adds the subscriber.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="subscriberCallbackReference">The subscriber callback reference.</param>
        public void AddSubscriber(string topic, ISubscription subscriberCallbackReference)
        {
            this.proxyManager.NotifyHost(string.Format("New subscriber for the topic {0}.", topic));

            lock (typeof(PubSubFilter))
            {
                if (this.SubscribersList.ContainsKey(topic))
                {
                    if (!this.SubscribersList[topic].Contains(subscriberCallbackReference))
                    {
                        this.SubscribersList[topic].Add(subscriberCallbackReference);
                    }
                }
                else
                {
                    var newSubscribersList = new List<ISubscription>();
                    newSubscribersList.Add(subscriberCallbackReference);
                    this.SubscribersList.Add(topic, newSubscribersList);
                }
            }
        }

        /// <summary>
        /// Removes the subscriber.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="subscriberCallbackReference">The subscriber callback reference.</param>
        public void RemoveSubscriber(string topic, ISubscription subscriberCallbackReference)
        {
            this.proxyManager.NotifyHost(string.Format("Subscriber removed from the topic {0}.", topic));

            lock (typeof(PubSubFilter))
            {
                if (this.SubscribersList.ContainsKey(topic))
                {
                    if (this.SubscribersList[topic].Contains(subscriberCallbackReference))
                    {
                        this.SubscribersList[topic].Remove(subscriberCallbackReference);
                    }
                }
            }
        }
        #endregion

        #region "Private Methods"

        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <returns>List of subscribers</returns>
        private List<ISubscription> GetSubscribers(string topic)
        {
            lock (typeof(PubSubFilter))
            {
                if (this.SubscribersList.ContainsKey(topic))
                {
                    return this.SubscribersList[topic];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}