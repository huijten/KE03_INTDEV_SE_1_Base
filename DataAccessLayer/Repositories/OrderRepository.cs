using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region Dependency Injection
        private readonly MatrixIncDbContext _context;

        public OrderRepository(MatrixIncDbContext context)
        {
            _context = context;
        }
        #endregion
        
        #region Add order
        
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        #endregion
        
        #region Delete order
        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        #endregion

        #region Get all orders
        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Parts);
        }
        #endregion

        #region Get order by id
        public Order? GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
        }
        #endregion
        
        #region Update order
        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        #endregion
    }
}
