using System.Text.Json.Serialization;

namespace authorizer.Domain;

public class Transaction
{
  [JsonPropertyName("merchant")]
  public string? Merchant { get; set; }
  [JsonPropertyName("amount")]
  public int Amount { get; set; }
  [JsonPropertyName("time")]
  public DateTime Time { get; set; }
  public bool Paid { get; set; }
}
