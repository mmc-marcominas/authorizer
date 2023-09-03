namespace authorizer.Domain;

public enum ExitCode
{
  /// <summary>
  /// Success on all operation
  /// </summary>
  Success = 0,
  /// <summary>
  /// Invalid argument error
  /// </summary>
  InvalidArgument = 1,
  /// <summary>
  /// Some operatin had failure during proccess
  /// </summary>
  OperationFailure = 2,
  /// <summary>
  /// Some unexpected exception happened
  /// </summary>
  UnhandledException = 3,
  /// <summary>
  /// Required data not found
  /// </summary>
  RequiredDataNotFound = 4,
}
