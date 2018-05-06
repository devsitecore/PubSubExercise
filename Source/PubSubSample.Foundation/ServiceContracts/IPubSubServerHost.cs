// <copyright file="IPubSubServerHost.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Foundation.ServiceContracts
{
    public interface IPubSubServerHost
    {
        void Notify(string message);
    }
}
