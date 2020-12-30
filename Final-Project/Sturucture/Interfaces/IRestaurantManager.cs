using Final_Project.Sturucture.Enums;
using Final_Project.Sturucture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.NewFolder
{
    interface IRestaurantManager
    {
        public  MenuItem MenuItem { get; set; }
        public List<Order> Orders { get; set; }
        public void AddOrder(string no, int coun);
        public void RemoveOrder(int orderNo);
        public void RemoveItem(string menuItemNo);
        public List<Order> GetOrdersByDatesInterval(DateTime fromDate, DateTime toDate);
        public List<Order> GetOrderByDate(DateTime date);
        public List<Order> GetOrdersByPriceInterval(double firstPrice, double secondPrice);
        public void GetOrderByNo(int no);
        public void AddMenuItem(string name, double price, Category category);
        public MenuItem EditMenuItem( string name, double price, string no);
        public List<MenuItem> GetMenuItemsCategory(Category category);
        public List<MenuItem> GetMenuItemsPriceInterval(double firstPrice, double secondPrice);
        public List<MenuItem> Search(string searchStr);
        public void GetMenuItems();
        public void GetOrders();
        public bool IsExistByName(string name);


    }
}
