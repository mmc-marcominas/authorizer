namespace authorizer.Domain;

public class TransactionSettings
{
  /// <summary>
  /// Small interval in minutes
  /// </summary>
  public int SmallIntervalInMinutes { get; internal set; }

  /// <summary>
  /// Max allowed on small interval
  /// </summary>
  public int MaxAllowedOnSmallInterval { get; internal set; }

  /// <summary>
  /// Transaction settings constructor
  /// </summary>
  public TransactionSettings()
  {
    SmallIntervalInMinutes = GeConfiguration("SMALL_INTERVAL_IN_MINUTES", 2) * -1;
    MaxAllowedOnSmallInterval = GeConfiguration("MAX_ALLOWED_ON_SMALL_INTERVAL", 3);
  }

  private static int GeConfiguration(string configurationKey, int defaultValue)
  {
    if (int.TryParse(Environment.GetEnvironmentVariable(configurationKey), out int value))
    {
      return Math.Abs(value);
    }

    return defaultValue;
  }
}
