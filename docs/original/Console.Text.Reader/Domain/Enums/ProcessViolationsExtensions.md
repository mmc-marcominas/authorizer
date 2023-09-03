# ProcessViolationsExtensions.cs
``` csharp
using System.Collections.Generic;

namespace Console.Text.Reader {
    public static class ProcessViolationsExtensions {

        private static Dictionary<ProcessViolations, string> ProcessViolationsDescriptions = new Dictionary<ProcessViolations, string> {
            { ProcessViolations.AccountAlreadyInitialized, "account-already-initialized" },
            { ProcessViolations.AccountNotInitialized, "account-not-initialized" },
            { ProcessViolations.CardNotActive, "card-not-active" },
            { ProcessViolations.InsufficientLimit, "insufficient-limit" },
            { ProcessViolations.HighFrequencySmallInterval, "high-frequency-small-interval" },
            { ProcessViolations.DoubleTransaction, "double-transaction" }
        };

        public static string GetDescription(this ProcessViolations processViolations) {
            ProcessViolationsDescriptions.TryGetValue(processViolations, out string value);
            return value;
        }
    }
}
```
