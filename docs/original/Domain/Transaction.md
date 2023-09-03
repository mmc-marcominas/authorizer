# Transaction.cs
``` csharp
using System;
using System.Text.Json.Serialization;

namespace Console.Text.Reader.Domain
{
    public class TransactionInfo
    {
        [JsonPropertyName("transaction")]
        public Transaction Transaction { get; set; }
    }

    public class Transaction
    {
        [JsonPropertyName("merchant")]
        public string Merchant { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        public bool Paid { get; set; }
    }
}
```
