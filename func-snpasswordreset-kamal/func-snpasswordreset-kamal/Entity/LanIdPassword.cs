//using Microsoft.Azure.Cosmos.Table;
using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace func_snpasswordreset_kamal.Entity
{
    public class LanIdPassword : ITableEntity
    {
        public LanIdPassword()
        {

        }
        public LanIdPassword(string partition, string row)
        {
            PartitionKey = partition;
            RowKey = row;
        }

        public string EncryptedPassword { get; set; }
        public string Recipient { get; set; }
        public bool IsViewed { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

    }
}
