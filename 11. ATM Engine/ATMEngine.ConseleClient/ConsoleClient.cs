namespace ATMEngine.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;

    using ATMEngine.Data;
    using ATMEngine.Model;

    public class ConsoleClient
    {
        public static void Main()
        {
            //FillWithSampleAccounts();

            CardAccount sampleValidAccount = new CardAccount { CardNumber = "9876542652", CardPin = "4567" };

            // CardAccount sampleInvalidAccount = new CardAccount { CardNumber = "4312343555", CardPin = "1234" };

            TransactionOptions transOptions = new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead };

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                try
                {
                    WithdrawMoneyFromAccount(sampleValidAccount, 200);
                    Console.WriteLine("The amount is withdrawed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Transaction failed!",
                        ex.Message);
                }

                scope.Complete();
            }
        }

        private static void WithdrawMoneyFromAccount(CardAccount cardAccount, decimal amount)
        {
            ATMEngineDbContex dbContex = new ATMEngineDbContex();

            using (dbContex)
            {
                var account = dbContex.CardAccounts.FirstOrDefault(a => a.CardPin == cardAccount.CardPin && a.CardNumber == cardAccount.CardNumber);

                if (account == null)
                {
                    Console.WriteLine("Not a valid account!");
                    dbContex.Dispose();
                    throw new ArgumentOutOfRangeException();
                }

                if (account.CardCash <= amount)
                {
                    Console.WriteLine("Insufficient value!");
                    dbContex.Dispose();
                    throw new ArgumentOutOfRangeException();
                }

                account.CardCash -= amount;
                account.Transactions.Add(new TransactionsHistory
                                            {
                                                CardNumber = account.CardNumber,
                                                TransactionDate = DateTime.Now,
                                                Amount = amount
                                            });
                dbContex.SaveChanges();
            }
        }


        private static void FillWithSampleAccounts()
        {
            var cardAccounts = new List<CardAccount>() {
                new CardAccount
                    {
                        CardNumber = "1234567890",
                        CardPin = "1234",
                        CardCash = 1000
                    },
                    new CardAccount
                    {
                        CardNumber = "9876542652",
                        CardPin = "4567",
                        CardCash = 11000
                    },
                    new CardAccount
                    {
                        CardNumber = "4325525412",
                        CardPin = "5423",
                        CardCash = 5000
                    }
            };

            ATMEngineDbContex dbContex = new ATMEngineDbContex();

            using (dbContex)
            {
                foreach (var cardAccount in cardAccounts)
                {
                    dbContex.CardAccounts.Add(cardAccount);
                }

                dbContex.SaveChanges();
            }
        }
    }
}
