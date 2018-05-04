// <copyright file="ISubscription.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.Contracts
{
    using System.ServiceModel;
    using DataContracts;

    /// <summary>
    /// ISubscription
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IPublishing))]
    public interface ISubscription
    {
        /// <summary>
        /// Subscribes the specified topic name.
        /// </summary>
        /// <param name="topicName">Name of the topic.</param>
        [OperationContract]
        void Subscribe(string topicName);

        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        /// <param name="topicName">Name of the topic.</param>
        [OperationContract]
        void UnSubscribe(string topicName);

        /// <summary>
        /// Notifies the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        [OperationContract]
        void Notify(PubSubMessage message);
    }
}
