using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public interface IEntity
    {
        int Id { get; }
    }

    public class Repository<T> where T : IEntity
    {
        protected IEnumerable<T> Elements;

        public Repository(IEnumerable<T> elements)
        {
            this.Elements = elements;
        }

        //generic for all entity orders,employes
        public T FindById(int id)
        {
            return Elements.SingleOrDefault(x => x.Id == id);
        }
    }


    public class Order : IEntity
    {
        public int Id { get; }
        public decimal Amount { get; }
    }

    public class OrderRepository : Repository<Order>
    {
        private readonly IList<Order> _orders;
        public OrderRepository(IList<Order> orders) : base(orders)
        {
            _orders = orders;
        }

        public IEnumerable<Order> FilterByAmount(decimal amount)
        {
            return _orders.Where(x => x.Amount == amount);
        }
    }
}
