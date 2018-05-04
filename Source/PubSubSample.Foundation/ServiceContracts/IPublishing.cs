// <copyright file="IPublishing.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.Contracts
{
    using System.ServiceModel;
    using Foundation.DataContracts;

    /// <summary>
    /// IPublishing
    /// </summary>
    [ServiceContract]
    public interface IPublishing
    {
        [OperationContract(IsOneWay = true)]
        void Publish(PubSubMessage e, string topicName);
    }
}
