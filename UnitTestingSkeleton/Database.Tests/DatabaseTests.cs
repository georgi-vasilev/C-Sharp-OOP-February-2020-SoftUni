using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private Database.Database database;
        private readonly int[] initialData = new int[] { 1, 2 };

        [SetUp]
        public void Setup()
        {
            this.database = new Database.Database(initialData);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]

        public void TestIfConstructorWorksProperly(int[] data)
        {
            this.database = new Database.Database(data);

            int expectedCount = data.Length;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ConstructorShouldThrowExceptionWithBiggerCollection()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5,
                                     6, 7, 8, 9, 10,
                                    11, 12, 13, 14,
                                    15, 16, 17 };

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database = new Database.Database(data);
            });
        }

        [Test]
        public void AddShouldIncreaseCountWhenAddedSuccessfully()
        {
            this.database.Add(3);

            int expectedCount = 3;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddShouldThrowExceptionWhenDatabaseIsFull()
        {
            for (int i = 3; i <= 16; i++)
            {
                this.database.Add(i);
            }

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Add(17);
            });
        }

        [Test]
        public void RemoveShouldDecreaseCountWhenSuccess()
        {
            //Arrange
            int expectedCount = 1;

            //act
            this.database.Remove();
            int actualCount = this.database.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        [Test]
        public void RemoveShouldThrowExceptionWhenDatabaseEmpty()
        {
            this.database.Remove();
            this.database.Remove();

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Remove();
            });
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
                                12, 13, 14, 15, 16 })]
        public void FetchSholdReturnCopyOfData(int[] expectedData)
        {
            this.database = new Database.Database(expectedData);

            int[] actualData = this.database.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}