using System.Text;

namespace authorizer.Infra;

public static class Output
{
  /// <summary>
  /// Write a line on standard output
  /// </summary>
  /// <param name="message"></param>
  public static void WriteLine(string message)
  {
    var bytes = Encoding.UTF8.GetBytes($"{message}\n");
    using var stdout = Console.OpenStandardOutput(bytes.Length);
    stdout.Write(bytes);
  }

  /// <summary>
  /// Write a line on standard error output
  /// </summary>
  /// <param name="error"></param>
  public static void WriteError(string error)
  {
    var errorMessage = Encoding.UTF8.GetBytes($"{error}\n");
    using var stderr = Console.OpenStandardError(errorMessage.Length);
    stderr.Write(errorMessage);
  }
}
