using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private readonly Orders _orders;
        private StringBuilder _stringBuilder;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            InitializeOrderContents();

            var contents = AppendOrderContents();

            return contents;
        }

        private string AppendOrderContents()
        {
            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                var order = _orders.GetOrder(i);
                AppendContentFor(order);
            }

            EndOrderContents();

            return _stringBuilder.ToString();
        }

        private void EndOrderContents()
        {
            if (_orders.GetOrdersCount() > 0)
            {
                _stringBuilder.Remove(_stringBuilder.Length - 2, 2);
            }

            _stringBuilder.Append("]}");
        }

        private void InitializeOrderContents()
        {
            _stringBuilder = new StringBuilder("{\"orders\": [");
        }

        private void AppendContentFor(Order order)
        {
            _stringBuilder.Append("{");
            _stringBuilder.Append("\"id\": ");
            _stringBuilder.Append(order.GetOrderId());
            _stringBuilder.Append(", ");
            _stringBuilder.Append("\"products\": [");

            AppendProductContentsFor(order);

            if (order.GetProductsCount() > 0)
            {
                _stringBuilder.Remove(_stringBuilder.Length - 2, 2);
            }

            _stringBuilder.Append("]");
            _stringBuilder.Append("}, ");
        }

        private void AppendProductContentsFor(Order order)
        {
            for (var j = 0; j < order.GetProductsCount(); j++)
            {
                var product = order.GetProduct(j);
                AppendContentFor(product);
            }
        }
        private void AppendContentFor(Product product)
        {
            var builder = BuildContentFor(product);

            _stringBuilder.Append(builder);
        }

        private static StringBuilder BuildContentFor(Product product)
        {
            var builder = new StringBuilder();
            builder.Append("{");

            var content = product.GetContent();

            var productContent = string.Join(", ", content.Select(
                TransformToJsonProperty));

            builder.Append(productContent);
            builder.Append("}, ");
            return builder;
        }

        private static string TransformToJsonProperty(KeyValuePair<string, string> item)
        {
            return string.Format("{0}: {1}", jsonEncode(item.Key), jsonEncode(
                item.Value));
        }

        private static string jsonEncode(string value)
        {
            double output;
            if (double.TryParse(value, out output))
            {
                return value;
            }
            return string.Format("\"{0}\"", value);
        }
    }
}