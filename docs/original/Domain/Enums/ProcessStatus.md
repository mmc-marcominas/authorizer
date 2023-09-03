# ProcessStatus.cs
``` csharp
namespace Console.Text.Reader {

    public enum ProcessStatus {
        InvalidArgs = -1,
        UnespectedError = -2,        
        Success = 0,
        FinishedWithUnacceptedTransaction = 1
    }
}
```
