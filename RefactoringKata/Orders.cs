using System.Collections.Generic;

namespace RefactoringKata
{
    public class Orders
    {
        private List<Order> _orders = new List<Order>();

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public int GetOrdersCount()
        {
            return _orders.Count;
        }

        public Order GetOrder(int i)
        {
            return _orders[i];
        }

        public List<Dictionary<string, object>> GetOrderContents()
        {
            var orderContents = new List<Dictionary<string, object>>();
            for (var i = 0; i < GetOrdersCount(); i++)
            {
                var order = GetOrder(i);
                orderContents.Add(order.GetContent());
            }
            return orderContents;
        }

        public Dictionary<string, object> GetContent()
        {
            var content = new Dictionary<string, object>
            {
                {"orders", GetOrderContents()}
            };
            return content;
        }
    }
}
