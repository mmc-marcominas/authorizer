# ProcessViolations.cs
``` csharp
namespace Console.Text.Reader {

    /// <summary>
    /// Process violations
    /// </summary>
    public enum ProcessViolations {

        /// <summary>
        /// Undefined process violation
        /// </summary>
        None,

        /// <summary>
        /// Account already initialized
        /// sample data: operations-account-already-initialized.json
        /// </summary>
        AccountAlreadyInitialized,

        /// <summary>
        /// Account not initialized
        /// sample data: operations-account-not-initialized.json
        /// </summary>
        AccountNotInitialized,

        /// <summary>
        /// Card not active
        /// sample data: operations-card-not-active.json
        /// </summary>
        CardNotActive,

        /// <summary>
        /// Insufficient limit
        /// sample data: operations-insufficient-limit.json
        /// </summary>
        InsufficientLimit,

        /// <summary>
        /// High frequency small interval
        /// sample data: operations-high-frequency-small-interval.json
        /// </summary>
        HighFrequencySmallInterval,

        /// <summary>
        /// Double transaction
        /// sample data: operations-double-transaction.json
        /// </summary>
        DoubleTransaction
    }
}
```
