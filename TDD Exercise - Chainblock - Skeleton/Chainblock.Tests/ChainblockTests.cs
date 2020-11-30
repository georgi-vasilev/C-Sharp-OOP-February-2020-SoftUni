namespace Chainblock.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Chainblock.Contracts;
    using Chainblock.Models;
    using Chainblock.Common;

    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;
        private ITransaction testTransaction;

        [SetUp]
        public void SetUp()
        {
            this.chainblock = new Core.ChainBlock();
            this.testTransaction = new Transaction(1,
                TransactionStatus.Unauthorized, "Pesho", "Gosho", 10);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            int expectedInitialCount = 0;

            IChainblock chainblock = new Core.ChainBlock();

            Assert.AreEqual(expectedInitialCount, chainblock.Count);
        }

        [Test]
        public void AddShouldIncreaseCountWhenSuccess()
        {
            int expectedCount = 1;

            ITransaction transaction = new Transaction(1,
                TransactionStatus.Successfull, "Pesho", "Gosho", 10);

            this.chainblock.Add(transaction);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void AddingExistingTransactionShouldThrowException()
        {
            ITransaction transaction = new Transaction(1,
                TransactionStatus.Failed, "Pesho", "Gosho", 10);

            this.chainblock.Add(transaction);

            Assert.That(() =>
            {
                this.chainblock.Add(transaction);
            },
            Throws
            .InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages
            .AddingExistingTransactionMessage));
        }

        [Test]
        public void AddingSameTransactionWithAnotherIdShouldPass()
        {
            int expectedCount = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(1,
                ts, from, to, amount);

            ITransaction transactionCopy = new Transaction(2,
                ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(transactionCopy);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void ContaintsByTransactionShouldReturnTrueWithExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByTransactionShouldReturnFalseWithNonExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);


            Assert.IsFalse(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByIdShouldReturnTrueWithExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(id));
        }

        [Test]
        public void ContainsByIdShouldReturnFalseWithNonExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);


            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        public void TestChangingTransactionStatusOfExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            TransactionStatus newStatus =
                TransactionStatus.Successfull;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);

            this.chainblock.Add(transaction);

            //Act
            this.chainblock.ChangeTransactionStatus(id, newStatus);

            Assert.AreEqual(newStatus, transaction.Status);
        }

        [Test]
        public void ChangingTransactionStatusOfNonExistingTransactionShouldThrowException()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            int fakeId = 13;

            TransactionStatus newStatus =
                TransactionStatus.Successfull;

            ITransaction transaction = new Transaction(id,
                ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.That(() =>
            {
                this.chainblock.ChangeTransactionStatus(fakeId, newStatus);
            }, Throws
            .ArgumentException.With
            .Message.EqualTo(ExceptionMessages.ChangingStatusOfNonExistingTransaction));
        }

        [Test]
        public void GetByIdShouldReturnCorrectTransaction()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 20;

            ITransaction transaction = new Transaction(id, ts,
                from, to, amount);

            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            ITransaction returnedTransaction = this.chainblock
                .GetById(id);

            Assert.AreEqual(transaction, returnedTransaction);
        }

        [Test]
        public void GetByIdShouldThrowExceptionWhenTryingToFindNonExistingTransaction()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 20;

            int fakeId = 13;

            ITransaction transaction = new Transaction(id, ts,
                from, to, amount);

            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);


            Assert.That(() =>
            {
                this.chainblock.GetById(fakeId);
            }, Throws
            .InvalidOperationException.With.Message
            .EqualTo(ExceptionMessages.NonExistingTransactionMessage));
        }

        [Test]
        public void RemovingTransactionShouldDecreaseCount()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            int expectedCount = 1;
            ITransaction transaction = new Transaction(id, ts,
                from, to, amount);

            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void RemovingTransactionShouldPhysicallyRemoveTheTransaction()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            ITransaction transaction = new Transaction(id, ts,
                from, to, amount);

            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.That(() =>
            {
                this.chainblock.GetById(id);
            }, Throws
            .InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages
            .NonExistingTransactionMessage));
        }

        [Test]
        public void RemovingNonExistingTransactionShouldThrowException()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            ITransaction transaction = new Transaction(id, ts,
                from, to, amount);

            int fakeId = 13;

            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);


            Assert.That(() =>
            {
                this.chainblock.RemoveTransactionById(fakeId);
            }, Throws
            .InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages
            .RemovingNonExistingTransactionMessage));
        }

        [Test]
        public void GetByTransactionStatusShouldReturnOrderedCollectionWithRightTransactions()
        {
            ICollection<ITransaction> expectedTransactions
                = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;
                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                if (transactionStatus == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction successfullTransaction = new Transaction(5,
                TransactionStatus.Successfull, "Pesho5", "Gosho5", 15);
            expectedTransactions.Add(successfullTransaction);
            this.chainblock.Add(successfullTransaction);

            IEnumerable<ITransaction> actualTransactions =
                this.chainblock.GetByTransactionStatus
                (TransactionStatus.Successfull);

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void TestGettingTransactionsWithNoExistingStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock.GetByTransactionStatus
                (TransactionStatus.Successfull);
            }, Throws.InvalidOperationException.With.Message.
            EqualTo(ExceptionMessages
            .EmptyStatusTransactionCollectionMessage));
        }

        [Test]
        public void AllSendersByStatusShouldReturnCorrectOrderedCollection()
        {
            ICollection<ITransaction> expectedTransactions
               = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;
                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                if (transactionStatus == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction successfullTransaction = new Transaction(5,
               TransactionStatus.Successfull, "Pesho5", "Gosho5", 15);
            expectedTransactions.Add(successfullTransaction);
            this.chainblock.Add(successfullTransaction);

            IEnumerable<string> actualTransactionOut = this.chainblock
                .GetAllSendersWithTransactionStatus
                (TransactionStatus.Successfull);
            IEnumerable<string> expectedTransactionsOut = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .Select(tx => tx.From);

            CollectionAssert.AreEqual(expectedTransactionsOut, actualTransactionOut);
        }

        [Test]
        public void AllSendersByStatusShouldThrownAnExceptionWhenThereAreNoTransactionsWithGivenStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock.GetAllSendersWithTransactionStatus
                (TransactionStatus.Successfull);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages
            .EmptyStatusTransactionCollectionMessage));
        }

        [Test]
        public void AllReceiversByStatusShouldReturnCorrectOrderedCollection()
        {
            ICollection<ITransaction> expectedTransactions
               = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;
                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                if (transactionStatus == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction successfullTransaction = new Transaction(5,
               TransactionStatus.Successfull, "Pesho5", "Gosho5", 15);
            expectedTransactions.Add(successfullTransaction);
            this.chainblock.Add(successfullTransaction);

            IEnumerable<string> actualTransactionOut = this.chainblock
                .GetAllReceiversWithTransactionStatus
                (TransactionStatus.Successfull);
            IEnumerable<string> expectedTransactionsOut = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .Select(tx => tx.To);

            CollectionAssert.AreEqual(expectedTransactionsOut, actualTransactionOut);
        }

        [Test]
        public void AllReceiversByStatusShouldThrownAnExceptionWhenThereAreNoTransactionsWithGivenStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock.GetAllReceiversWithTransactionStatus
                (TransactionStatus.Successfull);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages
            .EmptyStatusTransactionCollectionMessage));
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithNoDuplicatedAmounts()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = (TransactionStatus)(i % 4);
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expectedTransactions.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithDuplicatedAmounts()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = (TransactionStatus)(i % 4);
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expectedTransactions.Add(currentTransaction);
            }

            ITransaction transaction = new Transaction(11,
                TransactionStatus.Successfull, "Gosho11",
                "Pesho11", 10);
            this.chainblock.Add(transaction);
            expectedTransactions.Add(transaction);

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithEmptyCollection()
        {
            IEnumerable<ITransaction> actualTransacitons = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            CollectionAssert.IsEmpty(actualTransacitons);
        }

        [Test]
        public void TestIfGetAllBySenderDescendingByAmountWorksCorrectly()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            string wantedSender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = wantedSender;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetBySenderOrderedByAmountDescending(wantedSender);

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void TestGetAllByNonExistingSenderDescendingByAmount()
        {
            string wantedSender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock
                .GetBySenderOrderedByAmountDescending
                (wantedSender);
            }, Throws.InvalidOperationException.With
            .Message.EqualTo(ExceptionMessages
            .NoTransactionsForGivenSenderMessage));
        }

        [Test]
        public void TestIfGetByReceiverDescendingByAmountThenByIdWorksCorrectlyWithNoDuplicatedAmounts()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            string wantedReceiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = wantedReceiver;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetByReceiverOrderedByAmountThenById(wantedReceiver);

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }


        [Test]
        public void TestIfGetByReceiverDescendingByAmountThenByIdWorksCorrectlyWithDuplicatedAmounts()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            string wantedReceiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = wantedReceiver;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            ITransaction transaction = new Transaction(11,
                TransactionStatus.Successfull, "Pesho11", wantedReceiver, 10);

            this.chainblock.Add(transaction);
            expectedTransactions.Add(transaction);

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetByReceiverOrderedByAmountThenById(wantedReceiver);

            expectedTransactions = expectedTransactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetByReceiverDescendingByAmountThenByIdShouldThrowExceptionsWhenNoTransactionsFound()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            string wantedReceiver = "Gosho";

            Assert.That(() =>
            {
                this.chainblock
                .GetByReceiverOrderedByAmountThenById
                (wantedReceiver);
            }, Throws.InvalidOperationException.With.Message
            .EqualTo(ExceptionMessages
            .NoTransactionsForGivenReceiverMessage));
        }

        [Test]
        public void TestChainBlockEnumerator()
        {
            ICollection<ITransaction> addTransaction
                = new List<ITransaction>();
            ICollection<ITransaction> actualTransaction
                = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                addTransaction.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            foreach (ITransaction tr in this.chainblock)
            {
                actualTransaction.Add(tr);
            }

            CollectionAssert.AreEqual(addTransaction, actualTransaction);
        }

        [Test]
        public void GetBySenderAndMinimumAmountDescendingShouldReturnCorrectCollecionWithoutDuplicatedAmount()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            string wantedSender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = wantedSender;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            int minimumAmount = 12;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetBySenderAndMinimumAmountDescending(wantedSender, minimumAmount);


            expectedTransactions = expectedTransactions
                .Where(tx => tx.From == wantedSender && tx.Amount > minimumAmount)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetBySenderAndMinimumAmountDescendingShouldReturnCorrectCollecionWithDuplicatedAmount()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            string wantedSender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = wantedSender;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            ITransaction transaction = new Transaction(11,
                TransactionStatus.Successfull, "Pesho11", wantedSender, 13);
            this.chainblock.Add(transaction);
            expectedTransactions.Add(transaction);

            int minimumAmount = 12;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetBySenderAndMinimumAmountDescending(wantedSender, minimumAmount);


            expectedTransactions = expectedTransactions
                .Where(tx => tx.From == wantedSender && tx.Amount > minimumAmount)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetBySenderAndMinimumAmountDescendingShouldThrowExceptionWhenNoTransactionFound()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            string wantedSender = "Pesho";
            int minimumAmount = 12;

            Assert.That(() =>
            {
                this.chainblock
                .GetBySenderAndMinimumAmountDescending
                (wantedSender, minimumAmount);
            }, Throws.InvalidOperationException.With.Message
            .EqualTo(ExceptionMessages
            .NoTransactionsForGivenSenderMessage));
        }

        [Test]
        public void GetAllInAmountRangeShouldReturnCollectionInTheGivenRange()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 15; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            int low = 10;
            int high = 17;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetAllInAmountRange(low, high);

            expectedTransactions = expectedTransactions
                .Where(tx => tx.Amount >= low && tx.Amount <= high)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetAllInRangeShouldReturnEmptyCollectionIfNoTransactionsAreFound()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            int low = 50;
            int high = 100;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetAllInAmountRange(low, high);

            expectedTransactions = expectedTransactions
                .Where(tx => tx.Amount >= low && tx.Amount <= high)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
            CollectionAssert.IsEmpty(actualTransaction);
        }

        [Test]
        public void GetByReceiverAndAmountRangeShouldReturnCorrectCollection()
        {
            ICollection<ITransaction> expectedTransactions =
               new List<ITransaction>();

            string wantedReceiver = "Gosho";

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = wantedReceiver;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 10; i < 14; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            int low = 10;
            int high = 20;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetByReceiverAndAmountRange(wantedReceiver, low, high);


            expectedTransactions = expectedTransactions
                .Where(tx => (tx.To == wantedReceiver)
                && (tx.Amount >= low && tx.Amount < high))
                .OrderByDescending(tx => tx.Amount)
                .ThenByDescending(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetByReceierAndAmountRangeShouldThrowExceptionWhenNoTransactionsFound()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            string wantedSender = "Pesho";
            int low = 20;
            int high = 25;

            Assert.That(() =>
            {
                this.chainblock
                .GetByReceiverAndAmountRange
                (wantedSender, low, high);
            }, Throws.InvalidOperationException.With.Message
            .EqualTo(ExceptionMessages
            .NoTransactionsForGivenReceiverMessage));
        }

        [Test]
        public void GetByTransactionStatusAndMaximumAmountShouldReturnCorrectCollection()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 10; i < 14; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            int maximumAmount = 16;
            TransactionStatus wantedTransactionStatus =
                TransactionStatus.Successfull;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(wantedTransactionStatus, maximumAmount);


            expectedTransactions = expectedTransactions
                .Where(tx => tx.Status == TransactionStatus.Successfull 
                && tx.Amount <= maximumAmount)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
        }

        [Test]
        public void GetByTransactionStatusAndMaximumAmountShouldReturnEmptyCollectionWhenNoTransactionsFound()
        {
            ICollection<ITransaction> expectedTransactions =
                new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus transactionStatus = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id,
                    transactionStatus, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Transaction transaction = new Transaction(20,
                TransactionStatus.Unauthorized, "Pesho22", "Gosho22", 19);
            this.chainblock.Add(transaction);
            expectedTransactions.Add(transaction);

            int maximumAmount = 12;
            TransactionStatus wantedTransactionStatus =
                TransactionStatus.Unauthorized;

            IEnumerable<ITransaction> actualTransaction = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(wantedTransactionStatus, maximumAmount);


            expectedTransactions = expectedTransactions
                .Where(tx => tx.Status == TransactionStatus.Unauthorized
                && tx.Amount <= maximumAmount)
                .OrderByDescending(tx => tx.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransaction);
            CollectionAssert.IsEmpty(expectedTransactions);
        }
    }
}
