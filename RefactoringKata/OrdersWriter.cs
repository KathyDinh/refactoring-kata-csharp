using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var orderContents = new List<string>();
            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                var order = _orders.GetOrder(i);
                orderContents.Add(GetContentFor(order));
            }

            var content = string.Join(", ", orderContents);

            _stringBuilder.Append(content);

            EndOrderContents();

            return _stringBuilder.ToString();
        }

        private void EndOrderContents()
        {
            _stringBuilder.Append("]}");
        }

        private void InitializeOrderContents()
        {
            _stringBuilder = new StringBuilder("{\"orders\": [");
        }

        private string GetContentFor(Order order)
        {
            var content = new Dictionary<string, string>()
            {
                {"id", order.GetOrderId().ToString()}
                , {"products", GetProductContentsFor(order)}
            };

            var orderContent = TransformToAJsonObject(content);

            return orderContent;
        }

        private string GetProductContentsFor(Order order)
        {
            var productContents = order.GetProductContents();

            var json = GetJson(productContents);
            return json;
        }

        private static string GetJson(object content)
        {
            var jsonDefault = JsonConvert.SerializeObject(content);
            var json = jsonDefault.Replace(":", ": ")
                .Replace(",", ", ");
            return json;
        }

        private static string TransformToAJsonObject(Dictionary<string, string> content)
        {
            var jsonProperties = string.Join(", ", content.Select(
                TransformToJsonProperty));
            return string.Format("{{{0}}}", jsonProperties);
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

            if (value.StartsWith("["))
            {
                return value;
            }

            return string.Format("\"{0}\"", value);
        }
    }
}