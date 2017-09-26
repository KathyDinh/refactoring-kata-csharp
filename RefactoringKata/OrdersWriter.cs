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
            _stringBuilder.Append("{");
            _stringBuilder.Append("\"code\": \"");
            _stringBuilder.Append(product.Code);
            _stringBuilder.Append("\", ");
            _stringBuilder.Append("\"color\": \"");
            _stringBuilder.Append(product.getColor());
            _stringBuilder.Append("\", ");

            if (product.Size != Product.SIZE_NOT_APPLICABLE)
            {
                _stringBuilder.Append("\"size\": \"");
                _stringBuilder.Append(product.getSize());
                _stringBuilder.Append("\", ");
            }

            _stringBuilder.Append("\"price\": ");
            _stringBuilder.Append(product.Price);
            _stringBuilder.Append(", ");
            _stringBuilder.Append("\"currency\": \"");
            _stringBuilder.Append(product.Currency);
            _stringBuilder.Append("\"}, ");
        }
    }
}