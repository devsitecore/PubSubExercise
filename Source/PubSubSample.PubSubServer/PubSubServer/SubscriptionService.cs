// <copyright file="SubscriptionService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer
{
    using System;
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

        #region "Private Properties"
        private IPubSubFilter PubSubFilter { get; set; }
        #endregion

        #region ISubscription Members
        public void Subscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<ISubscription>();
            this.PubSubFilter.AddSubscriber(topic, subscriber);
        }

        public void UnSubscribe(string topic)
        {
            var subscriber = OperationContext.Current.GetCallbackChannel<ISubscription>();
            this.PubSubFilter.RemoveSubscriber(topic, subscriber);
        }

        public void Notify(PubSubMessage message)
        {
            throw new NotImplementedException();
        }

        public void Receive(PubSubMessage pubSubMessage)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}