using System.Text.Json;

using authorizer.Domain;

namespace authorizer.Extensions;

/// <summary>
/// Customer transactions extensions
/// </summary>
public static class CustomerTransactionsExtensions
{
  /// <summary>
  /// Validate <paramref name="data"/> sent and convert it in a <see cref="Account"/> 
  /// if possible storing in current <paramref name="customerTransactions"/>
  /// </summary>
  /// <param name="customerTransactions">A <see cref="CustomerTransactions"/> valid instance</param>
  /// <param name="data">A string value representing a <see cref="Account"/></param>
  public static void SetAccount(this CustomerTransactions customerTransactions, string data)
  {
    if (customerTransactions.Account != default)
    {
      customerTransactions.RegisterViolation(ProcessViolation.AccountAlreadyInitialized);
      return;
    }

    var accountInfo = JsonSerializer.Deserialize<AccountInfo>(data);
    if (accountInfo != default && accountInfo.Account != default)
    {
      customerTransactions.Account = new Account
      {
        ActiveCard = accountInfo.Account.ActiveCard,
        AvailableLimit = accountInfo.Account.AvailableLimit
      };
      customerTransactions.RegisterValidAccountOperation();
    }
  }

  /// <summary>
  /// Validate <paramref name="data"/> sent and convert it in a <see cref="Account"/> 
  /// applying it on <paramref name="customerTransactions"/> instance
  /// </summary>
  /// <param name="customerTransactions">A valid <see cref="CustomerTransactions"/> instance</param>
  /// <param name="data">A string value representing a <see cref="Transaction"/></param>
  public static void AddTransaction(this CustomerTransactions customerTransactions, string data)
  {
    var transactionInfo = JsonSerializer.Deserialize<TransactionInfo>(data);
    if (transactionInfo != default && transactionInfo.Transaction != default)
    {
      var transaction = new Transaction
      {
        Amount = transactionInfo.Transaction.Amount,
        Merchant = transactionInfo.Transaction.Merchant,
        Time = transactionInfo.Transaction.Time
      };
      if (customerTransactions.ValidateTransactionRegistration(transaction))
        customerTransactions.Transactions = customerTransactions.Transactions.Add(transaction);
    }
  }

  /// <summary>
  /// Register any rule violation acording validantion of received data
  /// </summary>
  /// <param name="customerTransactions">A valid <see cref="CustomerTransactions"/> instance</param>
  /// <param name="violation">Violation type, details <see cref="ProcessViolation"/></param>
  public static void RegisterViolation(this CustomerTransactions customerTransactions, ProcessViolation violation)
  {
    var violationtDescription = violation.GetDescription();
    var result = violation == ProcessViolation.AccountNotInitialized || customerTransactions.Account == default
                 ? Account.GetDetails(violationtDescription)
                 : customerTransactions.Account.ToString(violationtDescription);
    customerTransactions.OperationsResult = customerTransactions.OperationsResult.Add(result);
  }

  /// <summary>
  /// Register a valid <see cref="CustomerTransactions"/> operation - a operation sucessfuly finished
  /// </summary>
  /// <param name="customerTransactions">A valid <see cref="CustomerTransactions"/> instance</param>
  public static void RegisterValidAccountOperation(this CustomerTransactions customerTransactions)
  {
    if (customerTransactions.Account != default)
    {
      var result = customerTransactions.Account.ToString();
      customerTransactions.OperationsResult = customerTransactions.OperationsResult.Add(result);
    }
  }

  /// <summary>
  /// Validate <see cref="Transaction"/> registration rules
  /// </summary>
  /// <param name="customerTransactions">A valid <see cref="CustomerTransactions"/> instance</param>
  /// <param name="transaction">A valid <see cref="Transaction"/> instance</param>
  /// <returns></returns>
  private static bool ValidateTransactionRegistration(this CustomerTransactions customerTransactions, Transaction transaction)
  {
    var processViolation = customerTransactions.GetProcessViolation(customerTransactions.Account, transaction);

    if (processViolation == ProcessViolation.None)
    {
      if (customerTransactions.Account != default)
      {
        customerTransactions.Account.AvailableLimit -= transaction.Amount;
      }
      customerTransactions.RegisterValidAccountOperation();
      transaction.Paid = true;
    }
    else
    {
      customerTransactions.RegisterViolation(processViolation);
    }

    return customerTransactions.Account != default;
  }

  internal static ProcessViolation GetProcessViolation(this CustomerTransactions customerTransactions, Account? account, Transaction transaction)
  {
    if (account == default)
    {
      return ProcessViolation.AccountNotInitialized;
    }

    if (!account.ActiveCard)
    {
      return ProcessViolation.CardNotActive;
    }

    if (account.AvailableLimit < transaction.Amount)
    {
      return ProcessViolation.InsufficientLimit;
    }

    var transactionTimeLimit = transaction.Time.AddMinutes(customerTransactions.Settings.SmallIntervalInMinutes);

    if (customerTransactions.Transactions.Count(
             x => x.Paid &&
                  x.Time >= transactionTimeLimit) >= customerTransactions.Settings.MaxAllowedOnSmallInterval)
    {
      return ProcessViolation.HighFrequencySmallInterval;
    }

    if (customerTransactions.Transactions.Any(
             x => x.Paid && x.Amount == transaction.Amount &&
                  x.Merchant == transaction.Merchant &&
                  x.Time >= transactionTimeLimit))
    {
      return ProcessViolation.DoubleTransaction;
    }

    return ProcessViolation.None;
  }
}
