namespace authorizer.Domain;

public class CustomerTransactions
{
  public Account? Account { get; internal set; } = default;
  public IEnumerable<Transaction> Transactions { get; set; } = Array.Empty<Transaction>();
  public IEnumerable<string> OperationsResult { get; set; } = Array.Empty<string>();

  public bool SuccessOnAllOperations => !Transactions.Any(x => !x.Paid);
}
