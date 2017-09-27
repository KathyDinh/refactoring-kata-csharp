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
            var orderContents = new List<Dictionary<string,object>>();
            for (var i = 0; i < _orders.GetOrdersCount(); i++)
            {
                var order = _orders.GetOrder(i);
                var orderContent = order.GetContent();

                orderContents.Add(orderContent);
            }

            var json = GetJson(orderContents);

            _stringBuilder.Append(json);

            _stringBuilder.Append("]}");

            return _stringBuilder.ToString();
        }

        private void InitializeOrderContents()
        {
            _stringBuilder = new StringBuilder("{\"orders\": [");
        }

        private static string GetJson(object content)
        {
            var jsonDefault = JsonConvert.SerializeObject(content);
            var json = jsonDefault.Replace(":", ": ")
                .Replace(",", ", ");
            return json;
        }
     
    }
}