using System.Text.Json.Serialization;

namespace authorizer.Domain;

public class Account
{
  [JsonPropertyName("active-card")]
  public bool ActiveCard { get; set; }
  [JsonPropertyName("available-limit")]
  public int AvailableLimit { get; set; }

  public override string ToString()
  {
    return ToString(string.Empty);
  }

  public string ToString(string violation)
  {
    return GetDetails(violation, this);
  }
  public static string GetDetails(string violation, Account? account = default)
  {
    var replacement = string.IsNullOrWhiteSpace(violation)
                      ? string.Empty
                      : $"\"{violation}\"";
    var accountInfo = account == default
                      ? string.Empty
                      : $"\"active-card\": {account.ActiveCard}, \"available-limit\": {account.AvailableLimit}";
    return $"{{\"account\": {{{accountInfo}}}, \"violations\": [{replacement}]}}".ToLower();
  }
}
