using System.Text;
using Newtonsoft.Json;

namespace RefactoringKata
{
    public class OrdersWriter
    {
        private readonly Orders _orders;

        public OrdersWriter(Orders orders)
        {
            _orders = orders;
        }

        public string GetContents()
        {
            var json = GetJson(_orders);

            return json;
        }

        private static string GetJson(object content)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(JsonConvert.SerializeObject(content));

            //To comply with test output format
            stringBuilder.Replace(":", ": ");
            stringBuilder.Replace(",", ", ");
           
            return stringBuilder.ToString();
        }
     
    }
}