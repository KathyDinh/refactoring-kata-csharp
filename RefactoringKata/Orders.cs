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

        public static List<Dictionary<string, object>> GetOrderContents(Orders orders)
        {
            var orderContents = new List<Dictionary<string, object>>();
            for (var i = 0; i < orders.GetOrdersCount(); i++)
            {
                var order = orders.GetOrder(i);
                var orderContent = order.GetContent();

                orderContents.Add(orderContent);
            }
            return orderContents;
        }
    }
}
