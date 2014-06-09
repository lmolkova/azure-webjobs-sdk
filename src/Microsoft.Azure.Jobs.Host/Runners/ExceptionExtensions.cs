﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;

namespace Microsoft.Azure.Jobs.Host.Runners
{
    internal static class ExceptionExtensions
    {
        public static string ToDetails(this Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            // Additional details are only available on StorageException.
            IDictionary<string, string> additionalDetails = GetAdditionalDetails(exception);

            if (additionalDetails == null || additionalDetails.Count == 0)
            {
                return exception.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine(exception.ToString());

                builder.AppendLine("Additional Details:");

                foreach (KeyValuePair<string, string> detail in additionalDetails)
                {
                    builder.AppendFormat("{0}: {1}", detail.Key, detail.Value);
                    builder.AppendLine();
                }

                return builder.ToString();
            }
        }

        private static IDictionary<string, string> GetAdditionalDetails(Exception exception)
        {
            StorageException storageException = exception as StorageException;

            if (storageException == null)
            {
                return null;
            }

            RequestResult result = storageException.RequestInformation;

            if (result == null)
            {
                return null;
            }

            StorageExtendedErrorInformation extendedErrorInformation = result.ExtendedErrorInformation;

            if (extendedErrorInformation == null)
            {
                return null;
            }

            return extendedErrorInformation.AdditionalDetails;
        }
    }
}
