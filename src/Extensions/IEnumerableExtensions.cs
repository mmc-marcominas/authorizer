namespace authorizer.Extensions;

public static class IEnumerableExtensions
{
  public static IEnumerable<T> Add<T>(this IEnumerable<T> items, T newValue)
  {
    foreach (var item in items)
    {
      yield return item;
    }

    yield return newValue;
  }
}
