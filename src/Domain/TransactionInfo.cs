using System.Text.Json.Serialization;

namespace authorizer.Domain;

public class TransactionInfo
{
  [JsonPropertyName("transaction")]
  public Transaction? Transaction { get; set; }
}
