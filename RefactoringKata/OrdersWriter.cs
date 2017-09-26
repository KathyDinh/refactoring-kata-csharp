﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

            var content = new Dictionary<string, string>();
            content.Add("code", product.Code);
            content.Add("color", product.getColor());

            if (product.Size != Product.SIZE_NOT_APPLICABLE)
            {
                content.Add("size", product.getSize());
            }

            content.Add("price", product.Price.ToString(CultureInfo.InvariantCulture));
            content.Add("currency", product.Currency);
            var firstItem = true;
            foreach (var item in content)
            {
                if (!firstItem)
                {
                    _stringBuilder.Append(", ");
                }
                firstItem = false;
                _stringBuilder.AppendFormat("{0}: {1}", jsonEncode(item.Key), jsonEncode(item.Value));
            }
            _stringBuilder.Append("}, ");
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