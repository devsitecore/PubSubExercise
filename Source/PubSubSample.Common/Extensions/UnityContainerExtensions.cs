// <copyright file="UnityContainerExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Common.Extensions
{
    using global::Unity;

    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Resolves the specified container.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>Resolved Type Object</returns>
        public static T Resolve<T>(this UnityContainer container)
        {
            var returnObject = container.Resolve(typeof(T), string.Empty, null);

            if (!(returnObject is T))
            {
                returnObject = null;
            }

            return (T)returnObject;
        }
    }
}