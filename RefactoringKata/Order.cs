using System.Collections.Generic;

namespace RefactoringKata
{
    public class Order
    {
        private readonly int _id;
        private readonly List<Product> _products = new List<Product>();

        public int id
        {
            get { return _id; }
        }

        public List<Product> products
        {
            get { return _products; }
        }

        public Order(int id)
        {
            this._id = id;
        }

        public int GetOrderId()
        {
            return id;
        }

        public int GetProductsCount()
        {
            return _products.Count;
        }

        public Product GetProduct(int j)
        {
            return _products[j];
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public List<Dictionary<string, object>> GetProductContents()
        {
            var productContents = new List<Dictionary<string, object>>();
            for (var j = 0; j < GetProductsCount(); j++)
            {
                var product = GetProduct(j);
                productContents.Add(product.GetContent());
            }
            return productContents;
        }

        public Dictionary<string, object> GetContent()
        {
            var content = new Dictionary<string, object>()
            {
                {"id", GetOrderId()},
                {"products", GetProductContents()}
            };
            return content;
        }
    }
}