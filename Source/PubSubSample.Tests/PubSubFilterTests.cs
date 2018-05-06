using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PubSubSample.Foundation.Proxy;
using PubSubSample.Foundation.Base;
using PubSubSample.Tests.Mocking;
using PubSubSample.Foundation.ServiceContracts;
using PubSubSample.Tests.Base;
using PubSubSample.Common.Extensions;

namespace PubSubSample.Tests
{
    [TestClass]
    public class PubSubFilterTests : BaseTests
    {
        private bool isInitialized;

        private bool InitializeServicesHosting()
        {
            mockPubSubHost.Initialize(); //Clear messages

            if (!isInitialized)
            {
                isInitialized = this.pubSubServicesHostingManager.InitializeServicesHosting(mockPubSubHost);
            }

            return isInitialized;
        }

        public PubSubFilterTests()
        {
        }

        [TestMethod]
        public void TestInitializeSubscriberNoServer()
        {
            var expected = true;

            var mockSubscriber = new MockSubscriberHost();
            var actual = false;

            try
            {
                mockSubscriber.Subscribe("TopicA");
            }
            catch
            {
                actual = true;
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestInitializeServiceHostingMessages()
        {
            var expected = 2;
            var initialized = InitializeServicesHosting();
            var actual = mockPubSubHost.Messages.Count;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestInitializePublisher()
        {
            var expected = true;

            InitializeServicesHosting();
            var mockPublisher = new MockPublisherHost();
            var actual = true;

            try
            {
                mockPublisher.Publish("Hello", "TopicA");
            }
            catch
            {
                actual = false;
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestPublishMessage()
        {
            var expected = true;

            InitializeServicesHosting();
            var mockPublisher = new MockPublisherHost();
            var actual = true;

            try
            {
                mockPublisher.Publish("Message", "TopicB");
            }
            catch
            {
                actual = false;
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestSubscribeToTopic()
        {
            var expected = true;

            InitializeServicesHosting();
            var mockSubscriber = this.container.Resolve<MockSubscriberHost>();
            var actual = true;

            try
            {
                mockSubscriber.Subscribe("TopicC");
            }
            catch
            {
                actual = false;
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestMessageIsPublishedToSubscriber()
        {
            var expected = "message1";
            var topic = "TopicD";

            InitializeServicesHosting();
            var mockPublisherHost = this.container.Resolve<MockPublisherHost>();
            var mockSubscriberHost = this.container.Resolve<MockSubscriberHost>();

            mockSubscriberHost.Subscribe(topic);
            mockPublisherHost.Publish(expected, topic);

            this.WaitFor(2000);
            var actual = mockSubscriberHost.PubSubMessages[0].EventData;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestMessageNotPublishedToSubscriberWithDifferentTopic()
        {
            var expected = 0;
            var topicPublished = "TopicD";
            var topicSubscribed = "TopicE";

            InitializeServicesHosting();
            var mockPublisherHost = this.container.Resolve<MockPublisherHost>();
            var mockSubscriberHost = this.container.Resolve<MockSubscriberHost>();

            mockSubscriberHost.PubSubMessages.Clear();
            mockSubscriberHost.Subscribe(topicSubscribed);
            mockPublisherHost.Publish("message", topicPublished);

            this.WaitFor(2000);
            var actual = mockSubscriberHost.PubSubMessages.Count;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestMessagePublishedToMultipleSubscribers()
        {
            var topic = Guid.NewGuid().ToString();

            var message = "New message to multiple subscribers";

            InitializeServicesHosting();
            var publisher = this.container.Resolve<MockPublisherHost>();
            var subscriber1 = this.container.Resolve<MockSubscriberHost>();
            var subscriber2 = this.container.Resolve<MockSubscriberHost>();

            subscriber1.PubSubMessages.Clear();
            subscriber2.PubSubMessages.Clear();

            subscriber1.Subscribe(topic);
            subscriber2.Subscribe(topic);

            publisher.Publish(message, topic);

            this.WaitFor(2000);

            var isTrue = (subscriber1.PubSubMessages[0].EventData == message && subscriber2.PubSubMessages[0].EventData == message);

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void TestMultipleMessagesToSubscriber()
        {
            var topic = Guid.NewGuid().ToString();

            var message1 = "New message to multiple subscribers 1";
            var message2 = "New message to multiple subscribers 2";

            InitializeServicesHosting();
            var publisher = this.container.Resolve<MockPublisherHost>();
            var subscriber1 = this.container.Resolve<MockSubscriberHost>();

            subscriber1.PubSubMessages.Clear();

            subscriber1.Subscribe(topic);

            publisher.Publish(message1, topic);
            publisher.Publish(message2, topic);

            this.WaitFor(2000);

            var isTrue = (subscriber1.PubSubMessages.Count == 2 && subscriber1.PubSubMessages[0].EventData == message1 && subscriber1.PubSubMessages[1].EventData == message2);

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void TestNoMessageAfterUbsubscribe()
        {
            var topic = Guid.NewGuid().ToString();
            var message1 = "New message to multiple subscribers 1";
            var message2 = "New message to multiple subscribers 2";

            InitializeServicesHosting();
            var publisher = this.container.Resolve<MockPublisherHost>();
            var subscriber1 = this.container.Resolve<MockSubscriberHost>();
            subscriber1.PubSubMessages.Clear();

            subscriber1.Subscribe(topic);
            publisher.Publish(message1, topic);
            this.WaitFor(2000);

            subscriber1.UnSubscribe(topic);
            publisher.Publish(message2, topic);
            this.WaitFor(2000);

            var isTrue = (subscriber1.PubSubMessages.Count == 1 && subscriber1.PubSubMessages[0].EventData == message1);

            Assert.IsTrue(isTrue);
        }

        [TestMethod]
        public void TestMultipleMessagesFromMultiplePublishers()
        {
            var topic = Guid.NewGuid().ToString();

            var message1 = "New message to multiple subscribers 1";
            var message2 = "New message to multiple subscribers 2";

            InitializeServicesHosting();
            var publisher1 = this.container.Resolve<MockPublisherHost>();
            var publisher2 = this.container.Resolve<MockPublisherHost>();
            var subscriber = this.container.Resolve<MockSubscriberHost>();

            subscriber.PubSubMessages.Clear();

            subscriber.Subscribe(topic);

            publisher1.Publish(message1, topic);
            publisher2.Publish(message1, topic);
            this.WaitFor(2000);

            publisher1.Publish(message2, topic);
            publisher2.Publish(message2, topic);
            this.WaitFor(2000);

            var isTrue = (subscriber.PubSubMessages.Count == 4);

            Assert.IsTrue(isTrue);
            Assert.AreEqual(message1, subscriber.PubSubMessages[0].EventData);
            Assert.AreEqual(message1, subscriber.PubSubMessages[1].EventData);
            Assert.AreEqual(message2, subscriber.PubSubMessages[2].EventData);
            Assert.AreEqual(message2, subscriber.PubSubMessages[3].EventData);
        }

        [TestMethod]
        public void TestMultipleSubscribersMultiplePublishers()
        {
            var topic1 = Guid.NewGuid().ToString();
            var topic2 = Guid.NewGuid().ToString();
            var topic3 = Guid.NewGuid().ToString();

            var message1 = "New message to multiple subscribers 1";
            var message2 = "New message to multiple subscribers 2";

            InitializeServicesHosting();
            var publisher1 = this.container.Resolve<MockPublisherHost>();
            var publisher2 = this.container.Resolve<MockPublisherHost>();

            var subscriber1 = this.container.Resolve<MockSubscriberHost>();
            var subscriber2 = this.container.Resolve<MockSubscriberHost>();
            var subscriber3 = this.container.Resolve<MockSubscriberHost>();

            subscriber1.PubSubMessages.Clear();
            subscriber2.PubSubMessages.Clear();
            subscriber3.PubSubMessages.Clear();

            // Subscriber 1-> topic 1, topic 2
            // Subscriber 2-> topic 1
            // Subscriber 3-> topic 3
            subscriber1.Subscribe(topic1);
            subscriber1.Subscribe(topic2);
            subscriber2.Subscribe(topic2);
            subscriber3.Subscribe(topic3);

            //message -> topic 1, topic 1, topic 2
            publisher1.Publish(message1, topic1);
            publisher2.Publish(message2, topic1);
            this.WaitFor(2000);
            publisher2.Publish(message2, topic2);
            this.WaitFor(2000);

            var isTrue = (subscriber1.PubSubMessages.Count == 3 && subscriber2.PubSubMessages.Count == 1 && subscriber3.PubSubMessages.Count == 0);

            Assert.IsTrue(isTrue);
            Assert.AreEqual(message1, subscriber1.PubSubMessages[0].EventData);
            Assert.AreEqual(message2, subscriber1.PubSubMessages[1].EventData);
            Assert.AreEqual(message2, subscriber1.PubSubMessages[2].EventData);
            Assert.AreEqual(message2, subscriber2.PubSubMessages[0].EventData);
        }
    }
}
