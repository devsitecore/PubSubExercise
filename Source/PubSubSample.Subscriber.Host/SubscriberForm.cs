// <copyright file="SubscriberForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Subscriber
{
    using System;
    using System.Configuration;
    using System.Windows.Forms;
    using Common.Encryption;
    using Common.Enums;
    using Foundation.Contracts;
    using Foundation.DataContracts;
    using Foundation.ServiceContracts;

    /// <summary>
    /// Subscriber
    /// </summary>
    public partial class Subscriber : Form, ISubscription
    {
        #region "Private Members"

        private int eventsCount = 0;

        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="Subscriber"/> class.
        /// </summary>
        /// <param name="proxyManager">The proxy manager.</param>
        /// <param name="encryption">The encryption.</param>
        public Subscriber(IProxyManager proxyManager, IEncryption encryption)
        {
            this.SimpleEncryption = encryption;
            this.ProxyManager = proxyManager;

            this.InitializeComponent();
            this.InitializeFormState();
            this.CreateProxy();
        }
        #endregion

        /// <summary>
        /// Gets or sets the events count.
        /// </summary>
        /// <value>
        /// The events count.
        /// </value>
        private int EventsCount
        {
            get
            {
                return this.eventsCount;
            }

            set
            {
                var count = this.eventsCount = value;
                this.txtEventsCount.Text = count.ToString();
            }
        }

        private IProxyManager ProxyManager { get; set; }

        private IEncryption SimpleEncryption { get; set; }

        private ISubscription SubscriptionChannel { get; set; }

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

        #region "ISubscription Members"

        /// <summary>
        /// Receive message
        /// </summary>
        /// <param name="pubSubMessage">pubSubMessage</param>
        public void Receive(PubSubMessage pubSubMessage)
        {
            this.AddNewMessageToList(pubSubMessage);
        }

        /// <summary>
        /// Subscribes the specified topic name.
        /// </summary>
        /// <param name="topicName">Name of the topic.</param>
        public void Subscribe(string topicName)
        {
            this.SubscriptionChannel.Subscribe(topicName);

            this.txtTopicName.Enabled = false;
            this.btnSubscribe.Enabled = false;
            this.btnUnubscribe.Enabled = true;
        }

        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        /// <param name="topicName">Name of the topic.</param>
        public void UnSubscribe(string topicName)
        {
            this.txtTopicName.Enabled = true;
            this.btnUnubscribe.Enabled = false;
            this.btnSubscribe.Enabled = true;

            this.SubscriptionChannel.UnSubscribe(topicName);
        }

        /// <summary>
        /// Notifies the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public void Notify(PubSubMessage e)
        {
            this.AddNewMessageToList(e);
        }
        #endregion

        #region "Private Methods"

        /// <summary>
        /// Adds the new message to list.
        /// </summary>
        /// <param name="message">The message.</param>
        private void AddNewMessageToList(PubSubMessage message)
        {
            if (message != null)
            {
                var plainMessage = this.SimpleEncryption.Decrypt(message.EventData);
                var itemNum = (this.lstEvents.Items.Count < 1) ? 0 : this.lstEvents.Items.Count;
                this.lstEvents.Items.Add((itemNum + 1).ToString());
                this.lstEvents.Items[itemNum].SubItems.AddRange(new string[] { message.TopicName.ToString(), plainMessage, message.EventDate.ToString() });

                this.EventsCount++;
            }
        }

        /// <summary>
        /// Initializes the state of the form.
        /// </summary>
        private void InitializeFormState()
        {
            this.EventsCount = 0;
            this.TopicName = "TopicA";
        }

        /// <summary>
        /// Creates the proxy.
        /// </summary>
        private void CreateProxy()
        {
            var subEndPoint = ConfigurationManager.AppSettings["EndpointAddress"];
            var callbackInstance = this;
            this.SubscriptionChannel = this.ProxyManager.CreateChannel<ISubscription>(subEndPoint, ChannelType.Duplex, callbackInstance);
        }
        #endregion

        #region "Events"

        /// <summary>
        /// Called when [subscribe].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnSubscribe(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TopicName))
                {
                    MessageBox.Show("Please Enter a Topic Name");
                    return;
                }

                this.Subscribe(this.TopicName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Called when [un subscribe].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnUnSubscribe(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TopicName))
                {
                    MessageBox.Show("Please Enter a Topic Name");
                    return;
                }

                this.UnSubscribe(this.TopicName);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClearAstaListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClearAstaListView_Click(object sender, EventArgs e)
        {
            this.lstEvents.Items.Clear();
        }
        #endregion
    }
}