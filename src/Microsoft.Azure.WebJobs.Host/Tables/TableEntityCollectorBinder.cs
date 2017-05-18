﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table;

namespace Microsoft.Azure.WebJobs.Host.Tables
{
    internal class TableEntityCollectorBinder<T> : IValueBinder, IWatchable
         where T : ITableEntity
    {
        private readonly IStorageTable _table;
        private readonly TableEntityWriter<T> _tableWriter;
        private readonly Type _valueType;

        public TableEntityCollectorBinder(IStorageTable table, TableEntityWriter<T> tableWriter, Type valueType)
        {
            if (tableWriter != null && !valueType.GetTypeInfo().IsAssignableFrom(tableWriter.GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException("value is not of the correct type.");
            }

            _table = table;
            _tableWriter = tableWriter;
            _valueType = valueType;
        }

        public Type Type
        {
            get { return _valueType; }
        }

        public IWatcher Watcher
        {
            get
            {
                return _tableWriter;
            }
        }

        public Task<object> GetValueAsync()
        {
            return Task.FromResult<object>(_tableWriter);
        }

        public string ToInvokeString()
        {
            return _table.Name;
        }

        public Task SetValueAsync(object value, CancellationToken cancellationToken)
        {
            return _tableWriter.FlushAsync(cancellationToken);
        }

        public ParameterLog GetStatus()
        {
            return _tableWriter.GetStatus();
        }
    }
}
