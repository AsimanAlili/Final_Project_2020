using Final_Project.Sturucture.Enums;
using Final_Project.Sturucture.Exceptions;
using Final_Project.Sturucture.Models;
using Final_Project.Sturucture.Interfaces;


using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Services
{
     public class RestaurantManager: IRestaurantManager
    {
        #region RestaurantManager
        public List<MenuItem> MenuItems { get; set; }
        public List<Order> Orders { get; set; }
        public RestaurantManager()
        {
            MenuItems = new List<MenuItem>();
            Orders = new List<Order>();
        }
        #endregion


        #region MenuItemMethods
        
        #region AddMenuItem
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

        #endregion

        #region EditMenuItem
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

        #endregion

        #region GetMenuItems
        public void GetMenuItems()
        {
            foreach (var item in MenuItems)
            {
                Console.WriteLine($"Name: {item.Name}\nNo: {item.No}\nCategory: {item.Category}\nPrice: {item.Price}");
            }
        }

        #endregion

        #region GetMenuItemsCategory
        public List<MenuItem> GetMenuItemsCategory(Category category)
        {
            List<MenuItem> menuItem = MenuItems.FindAll(i => i.Category.Equals(category));
            return menuItem;
        }

        #endregion

        #region GetMenuItemsPriceInterval
        public List<MenuItem> GetMenuItemsPriceInterval(double firstPrice, double secondPrice)
        {
            if (firstPrice > 0 && secondPrice > 0)
            {
                if (secondPrice < firstPrice)
                {

                    throw new InvalidPriceIntervalException("Price", "The *Second* price cannot be less than the *First*!");
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

        #endregion

        #region RemoveItem
        public void RemoveItem(string menuItemNo)
        {
            menuItemNo = menuItemNo.Trim().ToLower();
            MenuItem menuRemove = MenuItems.Find(i => i.No.ToLower().Trim().Equals(menuItemNo));
            MenuItems.Remove(menuRemove);
        }

        #endregion

        #region Search
        public List<MenuItem> Search(string searchStr)
        {
            searchStr = searchStr.Trim().ToLower();

            List<MenuItem> searchedMenuItem = MenuItems.FindAll(b => b.Name.ToLower().Contains(searchStr));

            return searchedMenuItem;
        }

        #endregion

        #endregion

        #region OrderMethods

        #region AddOrder
        public void AddOrder(List<OrderItem> orderItems,Order order)
        {
            foreach (var item in orderItems)
            {
                OrderItem orderItem = order.OrderItems.Find(i => i.Equals(item));
                if (orderItem==null)
                {
                    order.OrderItems.Add(item);
                }
            }

            Order order1 = Orders.Find(i => i.No.Equals(order.No));
            if (order1==null)
            {
                Orders.Add(order);
            }
           
        }
        #endregion

        #region GetOrderByDate
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
        #endregion

        #region GetOrderByNo
        public void GetOrderByNo(int no)
        {
            Order orderNo = Orders.Find(i => i.No.Equals(no));
            if (orderNo == null)
            {
                throw new OrderNotFoundException("Order:", "Does not exist!");

            }
            else
            {
                foreach (var item in orderNo.OrderItems)
                {
                    int totalCount = 0;
                    totalCount += item.Count;
                    Console.WriteLine($"OrderNo:  {orderNo.No}\nDate: {orderNo.Date}\nTotal Amount: { orderNo.TotalAmount}" +
                        $"\nMenu Items Count:  {totalCount}\nMenu item name: {item.MenuItem.Name}\nMenu item No: {item.MenuItem.No}" +
                        $"\nCategory: {item.MenuItem.Category}\nPrice: {item.MenuItem.Price}");
                }

            }

        }
        #endregion

        #region GetOrders
        public void GetOrders()
        {
            foreach (var item1 in Orders)
            {
                
                if (Orders.Count == 0)
                {
                    Console.WriteLine("Not found *Orders*");
                }
                else
                {
                    foreach (var orderItem  in Orders)
                    {
                        int totalCount = 0;
                        foreach (var item in orderItem.OrderItems)
                        {
                            totalCount += item.Count;
                        }
                        Console.WriteLine($"Order No: {orderItem.No}\nTotal Amount: {orderItem.TotalAmount}\nDate: {orderItem.Date}\nCount: {totalCount}");
                    }
                    
                }
               
            }

        }
        #endregion

        #region GetOrdersByDatesInterval
        public List<Order> GetOrdersByDatesInterval(DateTime fromDate, DateTime toDate)
        {
            if (fromDate != null && toDate != null)
            {
                if (fromDate < toDate)
                {
                    List<Order> dateInterval = Orders.FindAll(i => i.Date >= fromDate && i.Date <= toDate);
                    return dateInterval;
                }
                else
                {
                    throw new ServiceInvalidDateException("Date", "The *Second* date cannot be less than the *First* date!");
                }
            }
            else
            {
                throw new ServiceInvalidDateException("Date:", "Date incorrect input!");
            }

        }
        #endregion

        #region GetOrdersByPriceInterval
        public List<Order> GetOrdersByPriceInterval(double firstPrice, double secondPrice)
        {
            if (firstPrice > 0 && secondPrice > 0)
            {
                if (secondPrice > firstPrice)
                {
                    List<Order> priceInterval = Orders.FindAll(i => i.TotalAmount >= firstPrice && i.TotalAmount <= secondPrice);
                    return priceInterval;

                }
                else
                {
                    throw new ServiceInvalidPriceException("Price", "The *Second* price cannot be less than the *First* price!");
                }
            }
            else
            {
                throw new ServiceInvalidPriceException("Price:", "Price incorrect input!");
            }
        }
        #endregion

        #region RemoveOrder
        public void RemoveOrder(int orderNo)
        {
            Order orderRemove = Orders.Find(i => i.No.Equals(orderNo));
            Orders.Remove(orderRemove);
        }
        #endregion

        #region IsExistByName
        public bool IsExistByName(string no)
        {
            no = no.Trim().ToLower();
            return MenuItems.Exists(i => i.No.ToLower() == no);
        }
        #endregion
        #endregion


    }
}
