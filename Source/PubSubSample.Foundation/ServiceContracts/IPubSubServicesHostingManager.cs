// <copyright file="IPubSubServicesHostingManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.ServiceContracts
{
    public interface IPubSubServicesHostingManager
    {
        /// <summary>
        /// Initialize Services Hosting
        /// </summary>
        /// <param name="host">The host</param>
        /// <returns>True if initialization is good</returns>
        bool InitializeServicesHosting(IPubSubServerHost host);
    }
}
