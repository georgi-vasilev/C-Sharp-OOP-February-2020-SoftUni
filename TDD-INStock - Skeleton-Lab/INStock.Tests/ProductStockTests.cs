namespace INStock.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using INStock.Models;

    [TestFixture]
    public class ProductStockTests
    {
        private const string PRODUCT_LABEL = "Test";
        private const string ANOTHER_PRODUCT_LABEL = "Another test";
        private Product product;
        private Product anotherProduct;
        private ProductStock productStock;

        [SetUp]
        public void SetUpProduct()
        {
            this.product = new Product(PRODUCT_LABEL, 10, 1);
            this.anotherProduct = new Product(ANOTHER_PRODUCT_LABEL, 20, 5);
            this.productStock = new ProductStock();
        }

        [Test]
        public void AddProductShouldSaveTheProduct()
        {
            //Act
            this.productStock.Add(this.product);

            //Assert
            var productInStock = this.productStock.FindByLabel(PRODUCT_LABEL);

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(PRODUCT_LABEL));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void AddProductShouldThrowExceptionWithDuplicateLabels()
        {
            Assert.That(() =>
            {
                this.productStock.Add(this.product);
                this.productStock.Add(this.product);
            },
            Throws
                .Exception.InstanceOf<ArgumentException>()
                .With.Message.EqualTo($"A product with {PRODUCT_LABEL} label already exists."));
        }

        [Test]
        public void AddingTwoProductsShouldSaveThem()
        {
            //Act
            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            //Assert
            var firstProductInStock = this.productStock.FindByLabel(PRODUCT_LABEL);
            var secondProductInStock = this.productStock.FindByLabel(ANOTHER_PRODUCT_LABEL);

            Assert.That(firstProductInStock, Is.Not.Null);
            Assert.That(firstProductInStock.Label, Is.EqualTo(PRODUCT_LABEL));
            Assert.That(firstProductInStock.Price, Is.EqualTo(10));
            Assert.That(firstProductInStock.Quantity, Is.EqualTo(1));

            Assert.That(secondProductInStock, Is.Not.Null);
            Assert.That(secondProductInStock.Label, Is.EqualTo(ANOTHER_PRODUCT_LABEL));
            Assert.That(secondProductInStock.Price, Is.EqualTo(20));
            Assert.That(secondProductInStock.Quantity, Is.EqualTo(5));
        }

        [Test]
        public void RemoveShouldThrowExceptionWhenProductIsNull()
        {
            Assert.That(
                //Arrange & Act
                () => this.productStock.Remove(null),
                //Assert
                Throws
                    .Exception.InstanceOf<ArgumentException>()
                    .With.Message.EqualTo("Product cannot be null."));
        }

        [Test]
        public void RemoveShouldReturnTrueWhenProductIsRemoved()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();
            var productToRemove = this.productStock.Find(3);

            //Act
            var result = this.productStock.Remove(productToRemove);

            //Assert
            Assert.That(result, Is.True);
            Assert.That(this.productStock.Count, Is.EqualTo(4));
            Assert.That(this.productStock[3].Label, Is.EqualTo("5"));
        }

        [Test]
        public void RemoveShouldReturnFalseWhenProductIsNotFound()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();
            var productNotInStock = new Product(PRODUCT_LABEL, 10, 20);

            //Act
            var result = this.productStock.Remove(productNotInStock);

            //Assert
            Assert.That(result, Is.False);
            Assert.That(this.productStock.Count, Is.EqualTo(5));
        }

        [Test]
        public void ContainsShouldReturnTrueWhenProductExists()
        {
            //Arrange
            this.productStock.Add(this.product);

            //Act
            var result = this.productStock.Contains(this.product);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ContainsShouldReturnFalseWhenProductDoesNotExists()
        {
            //Act
            var result = this.productStock.Contains(this.product);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ContaintsShouldThrowExceptionWhenProductIsNull()
        {
            Assert.That(
                //Arrange & Act
                () => this.productStock.Contains(null),
                //Assert
                Throws
                    .Exception.InstanceOf<ArgumentException>()
                    .With.Message.EqualTo("Product cannot be null"));
        }

        [Test]
        public void CountShouldReturnCorrectProductCount()
        {
            //Arrange
            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            //Act
            var result = this.productStock.Count;

            //Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void FindShouldReturnCorrectProductByIndex()
        {
            //Arrange
            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            //Act
            var productInStock = this.productStock.Find(1);

            //Assert
            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(ANOTHER_PRODUCT_LABEL));
            Assert.That(productInStock.Price, Is.EqualTo(20));
            Assert.That(productInStock.Quantity, Is.EqualTo(5));
        }

        [Test]
        public void FindShouldThrowExceptionWhenIndexIsOutOfRange()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock.Find(1),
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        [Test]
        public void FindShouldThrowExceptionWhenIndexIsBelowZero()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock.Find(-1),
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        [Test]
        public void FindByLabelShouldThrowExceptionWhenLabelIsNull()
        {
            Assert.That(
                //Arrange & Act
                () => this.productStock.FindByLabel(null),
                //Assert
                Throws
                    .Exception.InstanceOf<ArgumentException>()
                    .With.Message.EqualTo("Label cannot be null."));
        }

        [Test]
        public void FindByLabelShouldThrowExceptionWhenLabelDoesNotExist()
        {
            const string INVALID_LABEL = "Invalid label";

            Assert.That(
                //Arrange & Act
                () => this.productStock.FindByLabel(INVALID_LABEL),
                //Assert
                Throws
                    .Exception.InstanceOf<ArgumentException>()
                    .With.Message.EqualTo($"Product with {INVALID_LABEL} label could not be found."));
        }

        [Test]
        public void FindByLabelShouldReturnCorrectProduct()
        {
            //Arrange
            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            //Act
            var productInStock = this.productStock.FindByLabel(PRODUCT_LABEL);

            //Assert
            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(PRODUCT_LABEL));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void FindAllInPriceRangeShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock.FindAllInRange(60, 70);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindAllInPriceRangeShouldReturnCorrectCollectionWithCorrectOrder()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock
                .FindAllInRange(4, 21)
                .ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].Price, Is.EqualTo(20));
            Assert.That(result[1].Price, Is.EqualTo(10));
        }

        [Test]
        public void FindAllByPriceShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock.FindAllByPrice(30);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindAllByPriceShouldReturnCorrectCollection()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock
                .FindAllByPrice(400)
                .ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result[0].Label, Is.EqualTo("4"));
            Assert.That(result[1].Label, Is.EqualTo("5"));
        }

        [Test]
        public void FindMostExpensiveProductShouldReturnCorrectProduct()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var productInStock = this.productStock.FindMostExpensiveProduct();

            //Assert
            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo("4"));
            Assert.That(productInStock.Price, Is.EqualTo(400));
            Assert.That(productInStock.Quantity, Is.EqualTo(4));
        }

        [Test]
        public void FindMostExpensiveProductShouldThrowExceptoinWhenProductStockIsEmpty()
        {
            Assert.That(
               //Arrange & Act
               () => this.productStock.FindMostExpensiveProduct(),
               //Assert
               Throws
                   .Exception.InstanceOf<InvalidOperationException>()
                   .With.Message.EqualTo("Product stock is empty."));
        }

        [Test]
        public void FindAllByQuantityShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock.FindAllByQuantity(6);

            //Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindAllByQuantityShouldReturnCorrectCollection()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock
                .FindAllByQuantity(5)
                .ToList();

            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result[0].Label, Is.EqualTo("5"));
        }

        [Test]
        public void GetEnumeratorShouldReturnCorrectInsertionOrder()
        {
            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            var result = this.productStock.ToList();

            //Assert
            Assert.That(result[0].Label, Is.EqualTo("1"));
            Assert.That(result[1].Label, Is.EqualTo("2"));
            Assert.That(result[2].Label, Is.EqualTo("3"));
            Assert.That(result[3].Label, Is.EqualTo("4"));
            Assert.That(result[4].Label, Is.EqualTo("5"));
        }

        [Test]
        public void GetIndexReturnCorrectProductByIndex()
        {
            //Arrange
            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            //Act
            var productInStock = this.productStock[1];

            //Assert
            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(ANOTHER_PRODUCT_LABEL));
            Assert.That(productInStock.Price, Is.EqualTo(20));
            Assert.That(productInStock.Quantity, Is.EqualTo(5));
        }

        [Test]
        public void GetIndexThrowExceptionWhenIndexIsOutOfRange()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock[1],
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        [Test]
        public void GetIndexThrowExceptionWhenIndexIsBelowZero()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock[-1],
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        [Test]
        public void SetIndexShouldChangeProduct()
        {
            const string YET_PRODUCT_LABEL = "Yet another test";

            //Arrange
            this.AddMultipleProductsToProductStock();

            //Act
            this.productStock[3] = new Product(YET_PRODUCT_LABEL, 50, 3);

            //Assert
            var productInStock = this.productStock.Find(3);


            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(YET_PRODUCT_LABEL));
            Assert.That(productInStock.Price, Is.EqualTo(50));
            Assert.That(productInStock.Quantity, Is.EqualTo(3));

        }

        [Test]
        public void SetIndexThrowExceptionWhenIndexIsOutOfRange()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock[1] = new Product(PRODUCT_LABEL, 10, 10),
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        [Test]
        public void SetIndexThrowExceptionWhenIndexIsBelowZero()
        {
            //Arrange
            this.productStock.Add(this.product);

            Assert.That(
                //Act
                () => this.productStock[-1] = new Product(PRODUCT_LABEL, 10, 10),
                //Assert
                Throws
                    .Exception.InstanceOf<IndexOutOfRangeException>()
                    .With.Message.EqualTo("Product index does not exists."));
        }

        private void AddMultipleProductsToProductStock()
        {
            this.productStock.Add(new Product("1", 10, 1));
            this.productStock.Add(new Product("2", 50, 1));
            this.productStock.Add(new Product("3", 20, 1));
            this.productStock.Add(new Product("4", 400, 4));
            this.productStock.Add(new Product("5", 400, 5));
        }
    }
}
