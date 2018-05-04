// <copyright file="IPubSubFilter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.Contracts
{
    public interface IPubSubFilter : IPublishing
    {
        /// <summary>
        /// Removes the subscriber.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="subscriberCallbackReference">The subscriber callback reference.</param>
        void RemoveSubscriber(string topic, IPublishing subscriberCallbackReference);

        /// <summary>
        /// Adds the subscriber.
        /// </summary>
        /// <param name="topic">The topic.</param>
        /// <param name="subscriberCallbackReference">The subscriber callback reference.</param>
        void AddSubscriber(string topic, IPublishing subscriberCallbackReference);
    }
}
