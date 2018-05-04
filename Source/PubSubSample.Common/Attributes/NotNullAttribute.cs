// <copyright file="NotNullAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace PubSubSample.Common.Attributes
{
    using System;
    using System.Diagnostics;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute
    {
        public NotNullAttribute()
        {
            var param = this.MemberwiseClone();

            Debug.Assert(param != null, string.Format("Parameter cannot be null. {0}", this.TypeId));
        }
    }
}