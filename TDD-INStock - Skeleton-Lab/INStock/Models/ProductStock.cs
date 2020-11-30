namespace INStock.Models
{
    using INStock.Contracts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductStock : IProductStock
    {
        private readonly HashSet<string> productLabels;
        private readonly List<IProduct> productsByIndex;
        private readonly Dictionary<string, IProduct> productsByLabel;
        private readonly SortedDictionary<decimal, List<IProduct>> productsSortedByPrice;
        private readonly Dictionary<int, List<IProduct>> productsByQuantity;

        public ProductStock()
        {
            this.productLabels = new HashSet<string>();
            this.productsByIndex = new List<IProduct>();
            this.productsByLabel = new Dictionary<string, IProduct>();
            this.productsByQuantity = new Dictionary<int, List<IProduct>>();
            this.productsSortedByPrice = new SortedDictionary<decimal, List<IProduct>>(
                Comparer<decimal>.Create((first, second) => second.CompareTo(first)));
        }

        public int Count => this.productsByIndex.Count;

        public IProduct this[int index]
        {
            get => this.Find(index);
            set
            {
                this.ValidateNullProduct(value);

                this.RemoveProductFromCollections(this.Find(index));

                this.InitializeCollections(value);

                this.productsByIndex[index] = value;
            }
        }

        public void Add(IProduct product)
        {
            this.ValidateNullProduct(product);

            if (this.productLabels.Contains(product.Label))
            {
                throw new ArgumentException($"A product with {product.Label} label already exists.");
            }

            this.InitializeCollections(product);

            this.productLabels.Add(product.Label);
            this.productsByIndex.Add(product);
            this.productsByLabel[product.Label] = product;
            this.productsByQuantity[product.Quantity].Add(product);
            this.productsSortedByPrice[product.Price].Add(product);

        }

        public bool Contains(IProduct product)
        {
            this.ValidateNullProduct(product);

            return this.productLabels.Contains(product.Label);
        }

        public IProduct Find(int index)
        {
            if (index < 0 || index >=this.Count)
            {
                throw new IndexOutOfRangeException("Product index does not exists.");
            }

            return this.productsByIndex[index];
        }

        public IEnumerable<IProduct> FindAllByPrice(double price)
        {
            var priceAsDecimal = (decimal)price;

            if (!this.productsSortedByPrice.ContainsKey(priceAsDecimal))
            {
                return Enumerable.Empty<IProduct>();
            }

            return this.productsSortedByPrice[priceAsDecimal];
        }

        public IEnumerable<IProduct> FindAllByQuantity(int quantity)
        {

            if (!this.productsByQuantity.ContainsKey(quantity))
            {
                return Enumerable.Empty<IProduct>();
            }

            return this.productsByQuantity[quantity];
        }

        public IEnumerable<IProduct> FindAllInRange(double lo, double hi)
        {
            var result = new List<IProduct>();

            foreach (var (price, products) in this.productsSortedByPrice)
            {
                var priceAsDouble = (double)price;

                if (lo <= priceAsDouble && priceAsDouble <= hi)
                {
                    result.AddRange(products);
                }

                if (priceAsDouble < lo)
                {
                    break;
                }
            }

            return result;
        }

        public IProduct FindByLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                throw new ArgumentException("Label cannot be null.");
            }

            if (!this.productLabels.Contains(label))
            {
                throw new ArgumentException($"Product with {label} label could not be found.");
            }

            return this.productsByLabel[label];
        }

        public IProduct FindMostExpensiveProduct()
        {
            if (!this.productsSortedByPrice.Any())
            {
                throw new InvalidOperationException("Product stock is empty.");
            }
            var mostExpensiveProducts = this.productsSortedByPrice.First().Value;
            var firstAddedExpensiveProduct = mostExpensiveProducts.First();

            return firstAddedExpensiveProduct;
        }

        public IEnumerator<IProduct> GetEnumerator() => this.productsByIndex.GetEnumerator();

        public bool Remove(IProduct product)
        {
            this.ValidateNullProduct(product);

            var label = product.Label;

            if (!this.productLabels.Contains(label))
            {
                return false;
            }

            this.productsByIndex.RemoveAll(pr => pr.Label == label);
            this.RemoveProductFromCollections(product);

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void ValidateNullProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentException("Product cannot be null.");
            }
        }

        private void InitializeCollections(IProduct product)
        {
            if (!this.productsByQuantity.ContainsKey(product.Quantity))
            {
                this.productsByQuantity[product.Quantity] = new List<IProduct>();
            }

            if (!this.productsSortedByPrice.ContainsKey(product.Price))
            {
                this.productsSortedByPrice[product.Price] = new List<IProduct>();
            }
        }

        private void RemoveProductFromCollections(IProduct product)
        {
            var label = product.Label;

            this.productLabels.Remove(label);
            this.productsByLabel.Remove(label);

            var allWithProductQuantity = this.productsByQuantity[product.Quantity];
            allWithProductQuantity.RemoveAll(pr => pr.Label == label);

            var allWithProductPrice = this.productsSortedByPrice[product.Price];
            allWithProductPrice.RemoveAll(pr => pr.Label == label);
        }
    }
}
