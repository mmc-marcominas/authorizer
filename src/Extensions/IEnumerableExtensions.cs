namespace authorizer.Extensions;

/// <summary>
/// IEnumerable extensions
/// </summary>
public static class IEnumerableExtensions
{
  /// <summary>
  /// Add a item in a <see cref="IEnumerable"/>
  /// </summary>
  /// <typeparam name="T">IEnumerable type</typeparam>
  /// <param name="items">The <paramref name="items"/> instance</param>
  /// <param name="newValue">New value to be added</param>
  /// <returns></returns>
  public static IEnumerable<T> Add<T>(this IEnumerable<T> items, T newValue)
  {
    foreach (var item in items)
    {
      yield return item;
    }

    yield return newValue;
  }
}
