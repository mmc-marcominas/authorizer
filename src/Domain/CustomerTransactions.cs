namespace authorizer.Domain;

/// <summary>
/// Customer transactions
/// </summary>
public class CustomerTransactions
{
  /// <summary>
  /// Customer transactions <see cref="Account"/>
  /// </summary>
  public Account? Account { get; internal set; } = default;

  /// <summary>
  /// Customer transactions <see cref="Transaction"/> items
  /// </summary>
  public IEnumerable<Transaction> Transactions { get; set; } = Array.Empty<Transaction>();

  /// <summary>
  /// Customer transactions operations result
  /// </summary>
  public IEnumerable<string> OperationsResult { get; set; } = Array.Empty<string>();

  /// <summary>
  /// Customer transactions settings
  /// </summary>
  public TransactionSettings Settings { get; set; } = new();

  /// <summary>
  /// Customer transactions success on all operations logic operator
  /// </summary>
  public bool SuccessOnAllOperations => !Transactions.Any(x => !x.Paid);
}
