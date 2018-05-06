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
    public class MockPublisherHost : BaseTests, IPublishing
    {
        private IPublishing publishingChannel;

        public MockPublisherHost()
        {
            this.CreatePublishingChannel();
        }
        public void Publish(PubSubMessage message, string topicName)
        {
            this.publishingChannel.Publish(message, topicName);
        }

        public void Publish(string message, string topicName)
        {
            this.Publish(new PubSubMessage() { EventData = message, TopicName = topicName }, topicName);
        }

        private void CreatePublishingChannel()
        {
            var pubEndpointAddress = ConfigurationManager.AppSettings["PubEndpointAddress"];
            this.publishingChannel = this.proxyManager.CreateChannel<IPublishing>(pubEndpointAddress);
        }
    }
}
