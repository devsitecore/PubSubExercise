// <copyright file="SubscriptionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System.ServiceModel;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.DataContracts;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SubscriptionService : ISubscription
    {
        #region "Constructor"
        public SubscriptionService()
        {
            this.PubSubFilter = DependencyInjection.Instance().Container.Resolve<IPubSubFilter>();
        }
        #endregion

        private IPubSubFilter PubSubFilter { get; set; }

        #region ISubscription Members
        public void Subscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<IPublishing>();
            this.PubSubFilter.AddSubscriber(topic, subscriber);
        }

        public void UnSubscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<IPublishing>();
            this.PubSubFilter.RemoveSubscriber(topic, subscriber);
        }

        public void Notify(PubSubMessage message)
        {
            // IPublishing subscriber = OperationContext.Current.GetCallbackChannel<IPublishing>();
            // Filter.RemoveSubscriber(topicName, subscriber);
        }
        #endregion
    }
}