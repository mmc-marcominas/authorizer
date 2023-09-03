using authorizer.Domain;

namespace authorizer.Extensions;

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

  public static string GetDescription(this ProcessViolation ProcessViolation)
  {
    ProcessViolationDescriptions.TryGetValue(ProcessViolation, out string? value);
    return $"{value}";
  }
}
