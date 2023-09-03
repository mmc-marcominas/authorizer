using authorizer.Domain;

namespace authorizer.Extensions;

/// <summary>
/// Process violation extensions
/// </summary>
public static class ProcessViolationExtensions
{
  private static readonly Dictionary<ProcessViolation, string> ProcessViolationDescriptions = new()
  {
    { ProcessViolation.AccountAlreadyInitialized, "account-already-initialized" },
    { ProcessViolation.AccountNotInitialized, "account-not-initialized" },
    { ProcessViolation.CardNotActive, "card-not-active" },
    { ProcessViolation.InsufficientLimit, "insufficient-limit" },
    { ProcessViolation.HighFrequencySmallInterval, "high-frequency-small-interval" },
    { ProcessViolation.DoubleTransaction, "double-transaction" },
    { ProcessViolation.InvalidData, "invalid-data" }
  };

  /// <summary>
  /// Get Process violation description
  /// </summary>
  /// <param name="ProcessViolation">A <see cref="ProcessViolation"/> instance</param>
  /// <returns>Process violation description</returns>
  public static string GetDescription(this ProcessViolation ProcessViolation)
  {
    ProcessViolationDescriptions.TryGetValue(ProcessViolation, out string? value);
    return $"{value}";
  }
}
