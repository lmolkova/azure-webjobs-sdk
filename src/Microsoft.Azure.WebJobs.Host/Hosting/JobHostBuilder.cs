﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Azure.WebJobs.Hosting
{
    public class JobHostBuilder
    {
        public static IHostBuilder CreateDefault()
        {
            return new HostBuilder()
                .ConfigureWebJobsHost();
        }
    }
}