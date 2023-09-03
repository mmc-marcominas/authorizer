# CustomerTransactions.cs
``` csharp
using System.Collections.Generic;

namespace Console.Text.Reader.Domain
{
    public class CustomerTransactions {

        public CustomerTransactions() {
            Transactions = new Transaction[] { };
            OperationsResult = new string[] { };
            Account = default;
        }

        public Account Account { get; internal set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<string> OperationsResult { get; set; }
    }
}
```
