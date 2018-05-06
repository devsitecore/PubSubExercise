// <copyright file="PublisherForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Publisher
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;
    using Common.Encryption;
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;
    using Foundation.DataContracts;
    using Foundation.Proxy;
    using Foundation.ServiceContracts;

    /// <summary>
    /// PublisherForm
    /// </summary>
    public sealed partial class Publisher : Form, IPublishing
    {
        #region "Private Members"
        private int eventCounter = 0;
        #endregion

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        /// <param name="proxyManager">The proxy manager.</param>
        /// <param name="encryption">The encryption.</param>
        public Publisher(IProxyManager proxyManager, IEncryption encryption)
        {
            this.SimpleEncryption = encryption;
            this.ProxyManager = proxyManager;

            this.InitializeComponent();
            this.InitializeFormState();
            this.CreateProxy();
        }
        #endregion

        #region "Private Properties"

        /// <summary>
        /// Gets or sets the publishing proxy.
        /// </summary>
        /// <value>
        /// The publishing proxy.
        /// </value>
        private IPublishing PublishingChannel { get; set; }

        /// <summary>
        /// Gets or sets the simple encryption.
        /// </summary>
        /// <value>
        /// The simple encryption.
        /// </value>
        private IEncryption SimpleEncryption { get; set; }

        /// <summary>
        /// Gets or sets the proxy manager.
        /// </summary>
        /// <value>
        /// The proxy manager.
        /// </value>
        private IProxyManager ProxyManager { get; set; }

        /// <summary>
        /// Gets or sets the event counter.
        /// </summary>
        /// <value>
        /// The event counter.
        /// </value>
        private int EventCounter
        {
            get
            {
                return this.eventCounter;
            }

            set
            {
                this.eventCounter = value;
                this.txtEventCount.Text = this.eventCounter.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        private string Message
        {
            get
            {
                return this.txtEventData.Text.Trim();
            }

            set
            {
                this.txtEventData.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        /// <value>
        /// The name of the topic.
        /// </value>
        private string TopicName
        {
            get
            {
                return this.txtTopicName.Text.Trim();
            }

            set
            {
                this.txtTopicName.Text = value;
            }
        }
        #endregion

        #region IPublishing Members

        /// <summary>
        /// Publishes the specified e.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="topicName">Name of the topic.</param>
        public void Publish(PubSubMessage message, string topicName)
        {
            this.PublishingChannel.Publish(message, topicName);
            this.EventCounter++;
        }
        #endregion

        #region "Private Methods"

        /// <summary>
        /// Initializes the state of the form.
        /// </summary>
        private void InitializeFormState()
        {
            this.EventCounter = 0;
            this.TopicName = "TopicA";
            this.Message = "Test Message";
        }

        /// <summary>
        /// Creates the proxy.
        /// </summary>
        private void CreateProxy()
        {
            var pubEndpointAddress = ConfigurationManager.AppSettings["EndpointAddress"];
            this.PublishingChannel = this.ProxyManager.CreateChannel<IPublishing>(pubEndpointAddress);
        }

        /// <summary>
        /// Prepares the message.
        /// </summary>
        /// <returns>PubSubMessage</returns>
        private PubSubMessage PrepareMessage()
        {
            var encryptedMessage = this.SimpleEncryption.EncryptToString(this.Message);

            var message = DependencyInjection.Instance().Container.Resolve<PubSubMessage>();
            message.TopicName = this.TopicName;
            message.EventData = encryptedMessage;
            return message;
        }
        #endregion

        #region "Events"

        /// <summary>
        /// Called when [send event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSendEvent(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TopicName))
                {
                    MessageBox.Show("Please Enter a Topic.");
                    return;
                }

                var message = this.PrepareMessage();
                this.Publish(message, this.TopicName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Called when [reset counter].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnResetCounter(object sender, EventArgs e)
        {
            this.EventCounter = 0;
        }
        #endregion
    }
}