# Program.cs
``` csharp
using Console.Text.Reader.Commands;
using System;
using System.IO;
using System.Text;
using console = System.Console;

namespace Console.Text.Reader {

    public class Program {

        public static void Main() {
            var inputStream = console.OpenStandardInput();
            ProcessInputStream(inputStream);
        }

        public static void ProcessInputStream(Stream inputStream) {
            var reader = new StreamReader(inputStream);
            ProcessStatus exitCode;

            if (reader.Peek() <= 0) {
                exitCode = Usage();
            }
            else {
                exitCode = Consume(reader);
            }

            Environment.Exit((int)exitCode);
        }

        public static ProcessStatus Usage() {
            WriteLine("Usage: cat [filename.json] | Console.Text.Reader");
            return ProcessStatus.InvalidArgs;
        }

        private static void WriteLine(string message) {
            Write($"{message}\n");
        }

        private static void Write(string message) {
            var bytes = Encoding.UTF8.GetBytes(message);
            using var stdout = console.OpenStandardOutput(bytes.Length);
            stdout.Write(bytes);
        }

        private static ProcessStatus Consume(StreamReader reader) {

            try {
                var customerTransactionsCommand = new CustomerTransactionsCommand();
                customerTransactionsCommand.ProcessTransactions(reader);
                foreach (var result in customerTransactionsCommand.OperationsResult) {
                    WriteLine(result);
                }

                return customerTransactionsCommand.SuccessOnAllOperations() ? 
                           ProcessStatus.Success : 
                           ProcessStatus.FinishedWithUnacceptedTransaction;
            }
            catch (Exception ex) {
                LogError(ex.Message);
                return ProcessStatus.UnespectedError;
            }
        }
                
        private static void LogError(string error) {
            var errorMessage = Encoding.UTF8.GetBytes(error);
            using var stderr = console.OpenStandardError(errorMessage.Length);
            stderr.Write(errorMessage);
        }
    }
}
```
