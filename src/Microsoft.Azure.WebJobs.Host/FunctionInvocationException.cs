﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.Azure.WebJobs.Host
{
    /// <summary>
    /// Exception thrown when a job function invocation fails.
    /// </summary>
    public class FunctionInvocationException : FunctionException
    {
        /// <inheritdoc/>
        public FunctionInvocationException() : base()
        {
        }

        /// <inheritdoc/>
        public FunctionInvocationException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public FunctionInvocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="instanceId">The function instance Id.</param>
        /// <param name="methodName">The fully qualified method name.</param>
        /// <param name="innerException">The exception that is the cause of the current exception (or null).</param>
        public FunctionInvocationException(string message, Guid instanceId, string methodName, Exception innerException)
            : base(message, methodName, innerException)
        {
            InstanceId = instanceId;
        }

        /// <summary>
        /// Gets the instance Id of the failed invocation. This value can be correlated
        /// to the Dashboard logs.
        /// </summary>
        public Guid InstanceId { get; set; }
    }
}
