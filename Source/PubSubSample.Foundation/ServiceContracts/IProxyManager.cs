// <copyright file="IProxyManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.ServiceContracts
{
    using System.ServiceModel;
    using Common.Enums;

    public interface IProxyManager
    {
        /// <summary>
        /// Creates the chanel.
        /// </summary>
        /// <typeparam name="Service">The type of the ervice.</typeparam>
        /// <param name="endPoint">The end point.</param>
        /// <param name="channelType">Type of the channel.</param>
        /// <param name="callbackInstance">The callback instance.</param>
        /// <param name="securityMode">The security mode.</param>
        /// <returns>Chanel for the service</returns>
        Service CreateChannel<Service>(string endPoint, ChannelType channelType = ChannelType.Normal, object callbackInstance = null, SecurityMode securityMode = SecurityMode.None);

        /// <summary>
        /// Hosts the service.
        /// </summary>
        /// <typeparam name="Service">Service contract or type</typeparam>
        /// <typeparam name="Implementation">Implementation contract or type</typeparam>
        /// <param name="endPointAddress">The end point address.</param>
        /// <param name="securityMode">The security mode.</param>
        void HostService<Service, Implementation>(string endPointAddress, SecurityMode securityMode = SecurityMode.None);

        /// <summary>
        /// Notify host about different messages
        /// </summary>
        /// <param name="message">Message for the host</param>
        void NotifyHost(string message);

        /// <summary>
        /// Initialize the proxy manager to maintain a link between host
        /// </summary>
        /// <param name="pubSubServerHost">The server host.</param>
        void Initialize(IPubSubServerHost pubSubServerHost);
    }
}
