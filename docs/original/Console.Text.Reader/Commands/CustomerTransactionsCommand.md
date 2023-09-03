# CustomerTransactionsCommand.cs
``` csharp
using Console.Text.Reader.Domain;
using Console.Text.Reader.Infra;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Console.Text.Reader.Commands {
    public class CustomerTransactionsCommand {
        private readonly CustomerTransactions _customerTransactions;

        public IEnumerable<string> OperationsResult { 
            get { 
                return _customerTransactions.OperationsResult; 
            } 
        }

        public CustomerTransactionsCommand() {
            _customerTransactions = new CustomerTransactions();
        }

        public void ProcessTransactions(StreamReader reader) {
            if (reader.Peek() == 0)
                throw new RequiredDataNotFoundException();

            while (reader.Peek() > 0) {

                var line = reader.ReadLine().Trim();

                if (line.StartsWith("{\"account\":"))
                    SetAccount(line);
                else if (line.StartsWith("{\"transaction\":"))
                    AddTransaction(line);
            }
        }

        public bool SuccessOnAllOperations() {
            return _customerTransactions.Transactions.Any(x => !x.Paid);
        }

        private void SetAccount(string data) {

            if (_customerTransactions.Account != default) {
                RegisterViolation(ProcessViolations.AccountAlreadyInitialized);
                return;
            }
                

            var accountInfo = JsonSerializer.Deserialize<AccountInfo>(data);
            _customerTransactions.Account = new Account {
                ActiveCard = accountInfo.Account.ActiveCard,
                AvailableLimit = accountInfo.Account.AvailableLimit
            };
            RegisterValidOperation();
        }

        private void RegisterValidOperation() {
            var result = _customerTransactions.Account.ToString();
            _customerTransactions.OperationsResult = _customerTransactions.OperationsResult.Concat(new string[] { result });
        }

        private void RegisterViolation(ProcessViolations violation) {
            var violationtDescription = violation.GetDescription();
            var result = violation == ProcessViolations.AccountNotInitialized ?
                         Account.NullToString(violationtDescription) :
                         _customerTransactions.Account.ToString(violationtDescription);
            _customerTransactions.OperationsResult = _customerTransactions.OperationsResult.Concat(new string[] { result });
        }

        public void AddTransaction(string data) {
            var transactionInfo = JsonSerializer.Deserialize<TransactionInfo>(data);
            var transaction = new Transaction {
                Amount = transactionInfo.Transaction.Amount,
                Merchant = transactionInfo.Transaction.Merchant,
                Time = transactionInfo.Transaction.Time
            };
            if (ValidateTransactionRegistration(transaction))
                _customerTransactions.Transactions = _customerTransactions.Transactions.Concat(new Transaction[] { transaction });
        }

        private bool ValidateTransactionRegistration(Transaction transaction) {

            // TODO: think in other way to validate - focusing on KISS concept this
            //       implementation is acceptable but on large scale is not
            //       maintanable.
            var processViolations = ProcessViolations.None;
            var registerValidation = true;

            if (_customerTransactions.Account == default) {
                processViolations = ProcessViolations.AccountNotInitialized;
                registerValidation = false;
            }
            else if (!_customerTransactions.Account.ActiveCard) {
                processViolations = ProcessViolations.CardNotActive;
            }
            else if (_customerTransactions.Account.AvailableLimit < transaction.Amount) {
                processViolations = ProcessViolations.InsufficientLimit;
            }
            else if (_customerTransactions.Transactions.Count(
                     x => x.Paid &&
                          // TODO: move time to a configuration.
                          x.Time >= transaction.Time.AddMinutes(-2)) >= 3) {
                processViolations = ProcessViolations.HighFrequencySmallInterval;
            }
            else if (_customerTransactions.Transactions.Count(
                     x => x.Paid && x.Amount == transaction.Amount && 
                          x.Merchant == transaction.Merchant &&
                          // TODO: move time to a configuration.
                          x.Time >= transaction.Time.AddMinutes(-2)) > 0) {
                processViolations = ProcessViolations.DoubleTransaction;
            }

            if (processViolations == ProcessViolations.None) {
                _customerTransactions.Account.AvailableLimit -= transaction.Amount;
                RegisterValidOperation();
                transaction.Paid = true;
            }
            else {
                RegisterViolation(processViolations);
            }

            return registerValidation;
        }

    }
}
```
