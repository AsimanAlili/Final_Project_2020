using Final_Project.Sturucture.Enums;
using Final_Project.Sturucture.Exceptions;
using Final_Project.Sturucture.Models;
using Final_Project.Sturucture.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Services
{
     public class RestaurantManager: IRestaurantManager
    {
        public MenuItem MenuItem { get ; set ; }
        List<MenuItem> MenuItems { get; set; }
        List<Order> Orders { get; set; }
        
        List<Order> IRestaurantManager.Orders { get ; set ; }

        public RestaurantManager()
        {
            MenuItems = new List<MenuItem>();
            Orders = new List<Order>();
        }

        #region MenuItemMethods
        public void AddMenuItem(string name, double price, Category category)
        {
            string nameStr = name.Trim().ToLower();
            if (MenuItems.Exists(i => i.Name.Trim().ToLower() == nameStr))
            {
                throw new MenuItemAlreadyExistException("Name", "Menu items exist!");
            }

            MenuItem menuItem = new MenuItem(name, price, category);
            MenuItems.Add(menuItem);
        }
        public MenuItem EditMenuItem(string name, double price, string no)
        {
            MenuItem searched = MenuItems.Find(i => i.No.ToLower().Trim().Equals(no.Trim().ToLower()));
            string nameStr = name.ToLower().Trim();
            if (MenuItems.Exists(i => i.Name.Trim().ToLower() != nameStr))
            {
                searched.Price = price;
                searched.Name = name;
            }
            else
            {
                throw new MenuItemAlreadyExistException("Menu", "Menu item not found!");
            }
            return searched;
        }

        public void GetMenuItems()
        {
            foreach (var item in MenuItems)
            {
                Console.WriteLine($"Name: {item.Name}\nNo: {item.No}\nCategory: {item.Category}\nPrice: {item.Price}");
            }
        }

        public List<MenuItem> GetMenuItemsCategory(Category category)
        {
            List<MenuItem> menuItem = MenuItems.FindAll(i => i.Category.Equals(category));
            return menuItem;
        }

        public List<MenuItem> GetMenuItemsPriceInterval(double firstPrice, double secondPrice)
        {
            if (firstPrice > 0 && secondPrice > 0)
            {
                if (secondPrice < firstPrice)
                {

                    throw new InvalidPriceIntervalException("Price", "The second price cannot be less than the first!");
                }
                else
                {
                    List<MenuItem> menuPriceInterval = MenuItems.FindAll(i => i.Price >= firstPrice && i.Price <= secondPrice);
                    return menuPriceInterval;
                }

            }
            else
            {
                throw new InvalidPriceIntervalException("Price", "Prices cannot be less than zero");
            }

        }
        public void RemoveItem(string menuItemNo)
        {
            menuItemNo = menuItemNo.Trim().ToLower();
            MenuItem menuRemove = MenuItems.Find(i => i.No.ToLower().Trim().Equals(menuItemNo));
            MenuItems.Remove(menuRemove);
        }
        public List<MenuItem> Search(string searchStr)
        {
            searchStr = searchStr.Trim().ToLower();

            List<MenuItem> searchedMenuItem = MenuItems.FindAll(b => b.Name.ToLower().Contains(searchStr));

            return searchedMenuItem;
        }


        #endregion




        public void AddOrder(string no,int count)
        {
            if (count < 0) return;
            Order order = new Order();
            MenuItem item = MenuItems.Find(i => i.No.ToLower().Trim().Equals(no.Trim().ToLower()));

            OrderItem orderItem = new OrderItem(item, count);
            order.OrderItems.Add(orderItem);
            order.Sell(item.Name, orderItem.Count);

            Orders.Add(order);
        }

       

        public List<Order> GetOrderByDate(DateTime date)
        {

            if (date != null)
            {
                List<Order> dateOrder = Orders.FindAll(i => i.Date.Equals(date));
                return dateOrder;
                
            }
            else
            {
                throw new ServiceInvalidDateException("Date", "Enter the *Date* correctly!");
            }
        }

        public void GetOrderByNo(int no)
        {
            Order orderNo = Orders.Find(i => i.No.Equals(no));
            if (orderNo==null)
            {
                throw new OrderNotFoundException("Order", "Does not exist!");

            }
            else
            {
                foreach (var item in orderNo.OrderItems)
                {
                    Console.WriteLine($"OrderNo:  {orderNo.No}\nDate: {orderNo.Date}\nTotal Amount: { orderNo.TotalAmount}\nMenu Items Count:  {MenuItems.Count}");
                }

            }

        }

        public void GetOrders()
        {
            foreach (var item in Orders)
            {
                Console.WriteLine($"No: {item.No}\nTotal Amount: {item.TotalAmount}\nDate: {item.Date}");
            }
        }

        public List<Order> GetOrdersByDatesInterval(DateTime fromDate, DateTime toDate)
        {
            if (fromDate != null && toDate != null)
            {
                if (fromDate <toDate )
                {
                    List<Order> dateInterval = Orders.FindAll(i => i.Date >= fromDate && i.Date <= toDate);
                    return dateInterval;
                }
                else
                {
                    throw new ServiceInvalidDateException("Date", "The Second date cannot be less than the first date!");
                }
            }
            else
            {
                throw new ServiceInvalidDateException("Date", "Date incorrect input!");
            }
           
        }

        public List<Order> GetOrdersByPriceInterval(double firstPrice, double secondPrice)
        {
            if (firstPrice>0 && secondPrice>0)
            {
                if (secondPrice>firstPrice)
                {
                    List<Order> priceInterval = Orders.FindAll(i => i.TotalAmount >= firstPrice && i.TotalAmount <= secondPrice);
                    return priceInterval;

                }
                else
                {
                    throw new ServiceInvalidPriceException("Price", "The Second Price cannot be less than the first price!");
                }
            }
            else
            {
                throw new ServiceInvalidPriceException("Price", "Price incorrect input!");
            }
        }


        public void RemoveOrder(int orderNo)
        {
            Order orderRemove = Orders.Find(i => i.No.Equals(orderNo));
            Orders.Remove(orderRemove);
        }
        public bool IsExistByName(string no)
        {
            no = no.Trim().ToLower();
            return MenuItems.Exists(b => b.No.ToLower() == no);
        }

    }
}
