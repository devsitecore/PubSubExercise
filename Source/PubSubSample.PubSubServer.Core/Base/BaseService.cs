// <copyright file="BaseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.PubSubServer.Core.Base
{
    using Common.Extensions;
    using Common.Unity;
    using Foundation.Contracts;

    /// <summary>
    /// BaseService Class
    /// </summary>
    public abstract class BaseService
    {
        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="pubSubFilter">PubSubFilter</param>
        public BaseService(IPubSubFilter pubSubFilter)
        {
            this.PubSubFilter = pubSubFilter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
        {
            this.PubSubFilter = DependencyInjection.Instance().Container.Resolve<IPubSubFilter>();
        }
        #endregion

        #region "Protected Properties"

        /// <summary>
        /// Gets or sets PubSubFilter
        /// </summary>
        protected IPubSubFilter PubSubFilter { get; set; }
        #endregion

    }
}
