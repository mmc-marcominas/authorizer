using System.Text.Json;

using authorizer.Domain;

namespace authorizer.Extensions;

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
}
