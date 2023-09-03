using System.Text.Json.Serialization;

namespace authorizer.Domain;

internal class AccountInfo
{
  [JsonPropertyName("account")]
  public Account? Account { get; set; }
}
