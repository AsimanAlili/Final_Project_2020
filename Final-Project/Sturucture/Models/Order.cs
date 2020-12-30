using Final_Project.Sturucture.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Models
{
    public class Order
    {
        private int _no;
        private double _totalAmount;


        public int No { get { return _no; } } 
        public static int OrderCounter { get; set; }

        public double TotalAmount { get { return _totalAmount; } }
       
        public DateTime Date { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderCounter++;
            _no=OrderCounter;
            Date = DateTime.Now.Date;
            OrderItems = new List<OrderItem>();
        }
        public void Sell(string name, int count)
        {
            
            if (count <= 0) return;
            OrderItem orderItem= OrderItems.Find(o=>o.MenuItem.Name.Equals(name));

            
            if (orderItem == null) return;
           
            orderItem.Count = count;
            this._totalAmount+= count * orderItem.MenuItem.Price;
        }
       
    }
}
