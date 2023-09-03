# Account.cs
``` csharp
using System.Text.Json.Serialization;

namespace Console.Text.Reader.Domain
{
    internal class AccountInfo
    {
        [JsonPropertyName("account")]
        public Account Account { get; set; }
    }

    public class Account
    {
        [JsonPropertyName("active-card")]
        public bool ActiveCard { get; set; }
        [JsonPropertyName("available-limit")]
        public int AvailableLimit { get; set; }

        public override string ToString() {
            return ToString("");
        }

        public string ToString(string violation) {
            return NullToString(violation, this);
        }
        public static string NullToString(string violation, Account account = default) {
            var replacement = string.IsNullOrWhiteSpace(violation) ?
                              string.Empty :
                              $"\"{violation}\"";
            var accountInfo = account == default ? 
                              string.Empty:
                              $"\"active - card\": {account.ActiveCard}, \"available - limit\": {account.AvailableLimit}";
            return $"{{\"account\": {{{accountInfo}}}, \"violations\": [{replacement}]}}".ToLower();
        }
    }
}
```
