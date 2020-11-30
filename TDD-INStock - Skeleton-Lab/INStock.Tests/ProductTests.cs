namespace INStock.Tests
{
    using System;
    using NUnit.Framework;
    using INStock.Models;

    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void LabelCannotBeNull()
        {
            Assert.That(
                      //Arrange & Act
                      () => new Product(null, 10, 5),
                      //Assert
                      Throws
                      .Exception.InstanceOf<ArgumentException>()
                      .With.Message.EqualTo("Label cannot be null or empty."));
        }
        [Test]
        public void LabelCannotBeEmpty()
        {
            Assert.That(
                   //Arrange & Act
                   () => new Product(String.Empty, 10, 5),
                   //Assert
                   Throws
                   .Exception.InstanceOf<ArgumentException>()
                   .With.Message.EqualTo("Label cannot be null or empty."));
        }

        [Test]
        public void PriceCannotBeLessThanZero()
        {
            Assert.That(
                //Arrange & Act
                () => new Product("Test", -10, 5),
                //Assert
                Throws
                .Exception.InstanceOf<ArgumentException>()
                .With.Message.EqualTo("Price cannot be less than zero."));
        }

        [Test]
        public void QuantityCannotBeLessThanZero()
        {
            Assert.That(
                //Arrange & Act
                () => new Product("Test Product Label", 10, -1),
                //Assert
                Throws
                .Exception.InstanceOf<ArgumentException>()
                .With.Message.EqualTo("Quantity cannot be less than zero."));
        }

        [Test]
        public void ProductShouldBeComparedByPriceWhenOrderIsCorrect()
        {
            //Arrange
            var firstProduct = new Product("Test 1", 10, 1);
            var secondProduct = new Product("Test 2", 5, 1);

            //Act
            var correctOrderResult = secondProduct.CompareTo(firstProduct);

            //Assert
            Assert.That(correctOrderResult < 0, Is.True);
        }

        [Test]
        public void ProductShouldBeComparedByPriceWhenOrderIsIncorrect()
        {
            //Arrange
            var firstProduct = new Product("Test 1", 10, 1);
            var secondProduct = new Product("Test 2", 5, 1);

            //Act
            var incorrectOrderResult = firstProduct.CompareTo(secondProduct);

            //Assert
            Assert.That(incorrectOrderResult > 0, Is.True);
        }

        [Test]
        public void ProductShouldBeComparedByPriceWhenOrderIsEqual()
        {
            //Arrange
            var firstProduct = new Product("Test 1", 10, 1);
            var secondProduct = new Product("Test 2", 10, 1);

            //Act
            var incorrectOrderResult = firstProduct.CompareTo(secondProduct);

            //Assert
            Assert.That(incorrectOrderResult == 0, Is.True);
        }
    }
}