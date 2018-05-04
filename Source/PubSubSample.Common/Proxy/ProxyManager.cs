﻿// <copyright file="ProxyManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Common.Proxy
{
    using System.ServiceModel;
    using Enums;

    public class ProxyManager : IProxyManager
    {
        /// <summary>
        /// Creates the chanel.
        /// </summary>
        /// <typeparam name="Service">The type of the ervice.</typeparam>
        /// <param name="endPoint">The end point.</param>
        /// <param name="channelType">Type of the channel.</param>
        /// <param name="callbackInstance">The callback instance.</param>
        /// <param name="securityMode">The security mode.</param>
        /// <returns>
        /// Chanel for the service
        /// </returns>
        public Service CreateChannel<Service>(string endPoint, ChannelType channelType = ChannelType.Normal, object callbackInstance = null, SecurityMode securityMode = SecurityMode.None)
        {
            var endpointAddress = new EndpointAddress(endPoint);
            var netTcpBinding = new NetTcpBinding(securityMode);
            var context = new InstanceContext(callbackInstance);
            var channel = default(Service);

            if (channelType == ChannelType.Duplex)
            {
                var channelFactory = new DuplexChannelFactory<Service>(context, netTcpBinding, endpointAddress);
                channel = channelFactory.CreateChannel();
            }
            else
            {
                channel = ChannelFactory<Service>.CreateChannel(netTcpBinding, endpointAddress);
            }

            return channel;
        }

        /// <summary>
        /// Hosts the service.
        /// </summary>
        /// <typeparam name="Service">Service contract or type</typeparam>
        /// <typeparam name="Implementation">Implementation contract or type</typeparam>
        /// <param name="endPointAddress">The end point address.</param>
        /// <param name="securityMode">The security mode.</param>
        public void HostService<Service, Implementation>(string endPointAddress, SecurityMode securityMode = SecurityMode.None)
        {
            var serviceHost = new ServiceHost(typeof(Service));
            var tcpBinding = new NetTcpBinding(SecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(Implementation), tcpBinding, endPointAddress);
            serviceHost.Open();
        }
    }
}
