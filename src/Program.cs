using authorizer.Domain;
using authorizer.Infra;
using authorizer.Extensions;

namespace authorizer;

class Program
{
    /// <summary>
    /// Starter application endpoint
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        var stream = Console.OpenStandardInput();
        var reader = new StreamReader(stream);
        var peeking = reader.Peek();

        if (peeking <= 0 || peeking == 10)
        {
            Output.WriteLine("Usage: cat [filename.json] | ./path/to/authorizer");
            Output.WriteLine("Example: cat [filename.json] | ./path/to/authorizer");
            if (peeking <= 0)
                Output.WriteLine($"Received empty data, please check json content.");
            Environment.Exit((int)ExitCode.InvalidArgument);
        }

        var exitCode = reader.ProcessCustomerTransactions();
        Environment.Exit((int)exitCode);
    }
}
