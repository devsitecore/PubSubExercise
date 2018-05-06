using PubSubSample.Foundation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSubSample.Foundation.DataContracts;
using System.Configuration;
using PubSubSample.Tests.Base;
using PubSubSample.Common.Enums;

namespace PubSubSample.Tests.Mocking
{
    public class MockSubscriberHost : BaseTests, ISubscription
    {
        public List<PubSubMessage> PubSubMessages { get; private set; }
        private ISubscription subscriptionChannel;

        public MockSubscriberHost()
        {
            this.CreateSubscriptionChannel();
        }

        public void Receive(PubSubMessage pubSubMessage)
        {
            this.PubSubMessages.Add(pubSubMessage);
        }

        public void Subscribe(string topicName)
        {
            this.subscriptionChannel.Subscribe(topicName);
        }

        public void UnSubscribe(string topicName)
        {
            this.subscriptionChannel.UnSubscribe(topicName);
        }

        private void CreateSubscriptionChannel()
        {
            var subEndPointAddress = ConfigurationManager.AppSettings["SubEndpointAddress"];
            var callbackInstance = this;
            this.subscriptionChannel = this.proxyManager.CreateChannel<ISubscription>(subEndPointAddress, ChannelType.Duplex, callbackInstance);
            this.PubSubMessages = new List<PubSubMessage>();
        }
    }
}
