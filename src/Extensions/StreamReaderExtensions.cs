using authorizer.Domain;
using authorizer.Infra;

namespace authorizer.Extensions;

/// <summary>
/// StreamReader extensions
/// </summary>
public static class StreamReaderExtensions
{
  /// <summary>
  /// Get customer transactions and process them on received order
  /// More details, <see cref="ProcessTransactions"/>.
  /// </summary>
  /// <param name="reader"></param>
  /// <returns>Return processing status according <see cref="ExitCode"/> available values</returns>
  public static ExitCode ProcessCustomerTransactions(this StreamReader reader)
  {
    try
    {
      var customerTransactions = reader.ProcessTransactions();

      foreach (var result in customerTransactions.OperationsResult)
      {
        Output.WriteLine(result);
      }

      if (customerTransactions.SuccessOnAllOperations) 
        return ExitCode.Success;
      
      return ExitCode.OperationFailure;
    }
    catch (RequiredDataNotFoundException ex)
    {
      Output.WriteError(ex.Message);

      return ExitCode.RequiredDataNotFound;
    }
    catch (Exception ex)
    {
      Output.WriteError(ex.Message);

      return ExitCode.UnhandledException;
    }
  }

  /// <summary>
  /// Gets <paramref name="reader"/> <see cref="StreamReader"/>, read them and 
  /// process any <see cref="Account"/> or <see cref="Transaction"/> sent on it.
  /// </summary>
  /// <param name="reader"></param>
  /// <returns></returns>
  /// <exception cref="RequiredDataNotFoundException"></exception>
  public static CustomerTransactions ProcessTransactions(this StreamReader reader)
  {
    if (reader.Peek() == 0)
      throw new RequiredDataNotFoundException();

    var customerTransactions = new CustomerTransactions();
    
    while (reader.Peek() > 0)
    {
      var line = $"{reader.ReadLine()?.Trim()}";

      if (line.StartsWith("{\"account\":"))
        customerTransactions.SetAccount(line);
      else if (line.StartsWith("{\"transaction\":"))
        customerTransactions.AddTransaction(line);
      else
        customerTransactions.RegisterViolation(ProcessViolation.InvalidData);
    }

    return customerTransactions;
  }
}
