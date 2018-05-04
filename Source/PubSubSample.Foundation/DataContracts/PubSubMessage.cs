// <copyright file="PubSubMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Message
    /// </summary>
    [DataContract]
    [Serializable]
    public class PubSubMessage
    {
        public PubSubMessage()
        {
            this.EventDate = DateTime.UtcNow;
        }

        #region "Data Members"
        [DataMember]
        public DateTime EventDate
        {
            get;
            private set;
        }

        [DataMember]
        public string TopicName
        {
            get;
            set;
        }

        [DataMember]
        public string EventData
        {
            get;
            set;
        }
        #endregion
    }
}
