using Final_Project.Sturucture.Enums;
using Final_Project.Sturucture.Exceptions;
using Final_Project.Sturucture.Models;
using Final_Project.Sturucture.Services;

using System;
using System.Collections.Generic;

namespace Final_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            #region RestaurantManager
            RestaurantManager restaurantManager = new RestaurantManager();
            Console.WriteLine("=~=~=~=~=~=~=Restaurant=~=~=~=~=~=~=");
            bool oprChecker = true;
        MenuStart:
            if (!oprChecker)
            {
                Console.WriteLine("Error! Choose the right operation!");
                oprChecker = true;
            }
            Console.WriteLine("1.Perform an operation on the *Menu*");
            Console.WriteLine("2.Perform an operation on the *Order*");
            Console.WriteLine("0.Exit to system");
            Console.WriteLine("=~=~=~=~=~==~=~=~=~=~=~=~=~=~=~=~=~=");

            #endregion

            #region RestaurantManagerSwitch


            string selectedOpr = Console.ReadLine();

            switch (selectedOpr)
            {
                case "1":
                    #region MenuItemsChecker

                    Console.WriteLine("=~=~=~=~=~=~=~=~Menu~=~=~=~=~=~=~=~=");
                    bool oprMenuChecker = true;
                MenuItemStart:

                    if (!oprMenuChecker)
                    {
                        Console.WriteLine("Error! Choose the right operation!");
                        oprMenuChecker = true;
                    }

                    Console.WriteLine("1.Add new *Menu item*");
                    Console.WriteLine("2.Edit *Menu item*");
                    Console.WriteLine("3.Remove *Menu item*");
                    Console.WriteLine("4.Show all *Menu items*");
                    Console.WriteLine("5.Show menu items *Category*");
                    Console.WriteLine("6.Show menu items *Price interval*");
                    Console.WriteLine("7.Search menu items by *Name*");
                    Console.WriteLine("0.Go back to main menu");
                    Console.WriteLine("=~=~=~=~=~==~=~=~=~=~=~=~=~=~=~=~=~=");

                    #endregion

                    #region MenuItemSwitchCase
                    string menuOpt = Console.ReadLine();
                    switch (menuOpt)
                    {
                        case "1":
                            AddMenuItem(ref restaurantManager);
                            break;
                        case "2":
                            EditMenuItem(ref restaurantManager);
                            break;
                        case "3":
                            RemoveItem(ref restaurantManager);
                            break;
                        case "4":
                            ShowItems(ref restaurantManager);
                            break;
                        case "5":
                            ShowItemsByCategory(ref restaurantManager);
                            break;
                        case "6":
                            ShowItemsByPriceInterval(ref restaurantManager);
                            break;
                        case "7":
                            SearchItemsByName(ref restaurantManager);
                            break;

                        case "0":
                            goto MenuStart;

                        default:
                            oprMenuChecker = true;
                            break;
                    }
                    goto MenuItemStart;
                #endregion

                case "2":
                    #region OrderChecker
                    Console.WriteLine("=~=~=~=~=~=~=~=~Order=~=~=~=~=~=~=~=");

                    bool oprOrderChecker = true;
                OrderStart:

                    if (!oprOrderChecker)
                    {
                        Console.WriteLine("Error! Choose the right operation!");
                        oprOrderChecker = true;
                    }

                    Console.WriteLine("1.Add new *Order*");
                    Console.WriteLine("2.Remove *Order*");
                    Console.WriteLine("3.Show all *Order*");
                    Console.WriteLine("4.Show orders *Date* interval");
                    Console.WriteLine("5.Show orders *Price* interval");
                    Console.WriteLine("6.Show order by *Date*");
                    Console.WriteLine("7.Show  order by *Number*");
                    Console.WriteLine("0.Go back to main menu");
                    Console.WriteLine("=~=~=~=~=~==~=~=~=~=~=~=~=~=~=~=~=~=");

                    #endregion

                    #region OrderSwitchCase
                    string orderOpt = Console.ReadLine();
                    switch (orderOpt)
                    {
                        case "1":
                            AddOrder(ref restaurantManager);
                            break;
                        case "2":
                            RemoveOrder(ref restaurantManager);
                            break;
                        case "3":
                            ShowAllOrder(ref restaurantManager);
                            break;
                        case "4":
                            ShowOrderByDateInterval(ref restaurantManager);
                            break;
                        case "5":
                            ShowOrdersPriceInterval(ref restaurantManager);
                            break;
                        case "6":
                            ShowOrderByDate(ref restaurantManager);
                            break;
                        case "7":
                            ShowOrderByNo(ref restaurantManager);
                            break;
                        case "0":
                            goto MenuStart;
                        default:
                            oprMenuChecker = true;
                            break;
                    }
                    goto OrderStart;
                #endregion

                case "0":
                    return;
                default:
                    oprChecker = false;
                    break;

            }
            goto MenuStart;
            #endregion
        }
        #region MenuItemMethods
        #region AddMenuItem
        public static void AddMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Menu Item *Name*:");
            string itemName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(itemName))
            {
                Console.WriteLine("Error! Enter the item *Name* correctly!");
                itemName = Console.ReadLine();
            }

            Console.WriteLine("Enter the *Price*:");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                priceStr = Console.ReadLine();
            }


            Console.WriteLine("Select *Category*:");

            string[] categoryNames = Enum.GetNames(typeof(Category));
            for (int i = 0; i < categoryNames.Length; i++)
            {
                Console.WriteLine(" " + i + "-" + categoryNames[i]);
            }
            string selectedEnumStr = Console.ReadLine();

            int selectedEnumInt;

            while (!int.TryParse(selectedEnumStr, out selectedEnumInt) || selectedEnumInt < 0 || selectedEnumInt >= categoryNames.Length)
            {
                Console.WriteLine("Error! Enter the *Category* correctly!");
                selectedEnumStr = Console.ReadLine();
            }
            Category selectedCategory = (Category)selectedEnumInt;
            try
            {
                restaurantManager.AddMenuItem(itemName, price, selectedCategory);
                Console.WriteLine("Menu items added successfully!");
                Console.WriteLine("=~=~=~=~=~==~=~=~=~=~=~=~=~=~=~=~=");

            }
            catch (MenuItemAlreadyExistException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        #endregion

        #region EditMenuItem
        public static void EditMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *Number* you want to change:");
            string editStr = Console.ReadLine();
            
           
            while (!restaurantManager.IsExistByName(editStr))
            {
                Console.WriteLine("Error! Enter the *Number* correctly!");
                editStr = Console.ReadLine();
            }

            Console.WriteLine("Enter the *Price* you want to change:");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                priceStr = Console.ReadLine();
            }

            Console.WriteLine("Enter the *Name* you want to change:");
            string itemName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(itemName))
            {
                Console.WriteLine("Error! Enter the item *Name* correctly!");
                itemName = Console.ReadLine();
            }

            restaurantManager.EditMenuItem(itemName, price, editStr);





        }

        #endregion

        #region RemoveItem
        public static void RemoveItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *Number* you want to delete:");
            string removeStr = Console.ReadLine();
            while (!restaurantManager.IsExistByName(removeStr))
            {
                Console.WriteLine("Error! Enter the *Number* correctly!");
                removeStr = Console.ReadLine();
            }

            while (string.IsNullOrWhiteSpace(removeStr))
            {
                Console.WriteLine("Error! Enter the *Number* correctly!");

            }
            restaurantManager.RemoveItem(removeStr);
        }
        #endregion

        #region ShowItems
        public static void ShowItems(ref RestaurantManager restaurantManager)
        {
            restaurantManager.GetMenuItems();
        }
        #endregion

        #region ShowItemsByCategory
        public static void ShowItemsByCategory(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Select *Category*:");

            string[] categoryNames = Enum.GetNames(typeof(Category));
            for (int i = 0; i < categoryNames.Length; i++)
            {
                Console.WriteLine(" " + i + "-" + categoryNames[i]);
            }
            string selectedEnumStr = Console.ReadLine();

            int selectedEnumInt;

            while (!int.TryParse(selectedEnumStr, out selectedEnumInt) || selectedEnumInt < 0 || selectedEnumInt >= categoryNames.Length)
            {
                Console.WriteLine("Error! Enter the *Category* correctly!");
                selectedEnumStr = Console.ReadLine();
            }
            Category selectedCategory = (Category)selectedEnumInt;

            List<MenuItem> itemsCategory = restaurantManager.GetMenuItemsCategory(selectedCategory);
            foreach (var item in itemsCategory)
            {
                Console.WriteLine($"Name: {item.Name}\nNo: {item.No}\nCategory: {item.Category}\nPrice: {item.Price}");
            }
        }
        #endregion

        #region ShowItemsByPriceInterval
        public static void ShowItemsByPriceInterval(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *First* price:");
            string firstPriceStr = Console.ReadLine();
            double firstPrice;

            while (!double.TryParse(firstPriceStr, out firstPrice) || firstPrice <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                firstPriceStr = Console.ReadLine();
            }
            Console.WriteLine("Enter the *Second* price:");

            string secondPriceStr = Console.ReadLine();
            double secondPrice;

            while (!double.TryParse(secondPriceStr, out secondPrice) || secondPrice <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                secondPriceStr = Console.ReadLine();
            }
            List<MenuItem> itemsCategory = restaurantManager.GetMenuItemsPriceInterval(firstPrice, secondPrice);
            foreach (var item in itemsCategory)
            {
                Console.WriteLine($"Name: {item.Name}\nNo: {item.No}\nCategory: {item.Category}\nPrice: {item.Price}");
            }

        }
        #endregion

        #region SearchItemsByName
        public static void SearchItemsByName(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *Name* you are looking for:");
            string searchName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Error! Enter the *Search* correctly!");
                searchName = Console.ReadLine();
            }
            foreach (var item in restaurantManager.Search(searchName))
            {
                Console.WriteLine($"Name: {item.Name}\nNo: {item.No}\nCategory: {item.Category}\nPrice: {item.Price}");
            }

        }
        #endregion
        #endregion

        #region OrderMethods
        #region CreatOrderItems
        public static List<OrderItem> CreatOrderItems(MenuItem menuItem, List<OrderItem> orderItems, int count)
        {

            OrderItem orderItem = new OrderItem(menuItem, count);
            OrderItem orderItem1 = orderItems.Find(i => i.MenuItem.No == menuItem.No);
            if (orderItem1 == null)
            {
                orderItems.Add(orderItem);

            }
            else
            {
                orderItem1.Count += count;
            }
            return orderItems;

        }
        #endregion

        #region AddOrder

        public static void AddOrder(ref RestaurantManager restaurantManager)
        {
            Order order = new Order();
            List<OrderItem> orderItems = new List<OrderItem>();
            List<OrderItem> orderItems1 = new List<OrderItem>();
        AddOrder:
            Console.WriteLine("Enter Menu *Number*");
            string orderNo = Console.ReadLine();

            while (!restaurantManager.IsExistByName(orderNo))
            {
                Console.WriteLine("Error! Enter the *Number* correctly!");
                orderNo = Console.ReadLine();
            }


            while (string.IsNullOrWhiteSpace(orderNo))
            {
                Console.WriteLine("Error! Enter the order *Number* correctly!");
                orderNo = Console.ReadLine();

            }

            MenuItem menu = restaurantManager.MenuItems.Find(i => i.No.ToLower().Trim().Equals(orderNo.Trim().ToLower()));
            Console.WriteLine("Enter the *Count*:");
            string countStr = Console.ReadLine();
            int count;

            while (!int.TryParse(countStr, out count) || count < 1)
            {
                Console.WriteLine("Error! Enter the *Count* correctly!");
                countStr = Console.ReadLine();
            }

            foreach (var item in CreatOrderItems(menu, orderItems1, count))
            {
                OrderItem orderItem = orderItems.Find(i => i.MenuItem.Name == item.MenuItem.Name);
                if (orderItem == null)
                {
                    orderItems.Add(item);
                }
            }
            restaurantManager.AddOrder(orderItems, order);
            order.Sell(menu.Name, count);

            Console.WriteLine("Type *Yes* if you want to add");
            string add = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(orderNo))
            {
                Console.WriteLine("Error! Enter the order keys correctly!");
                add = Console.ReadLine();
               

            }
            string answer = "yes";
            if (add.ToLower().Trim() == answer.ToLower().Trim())
            {
                goto AddOrder;
            }
        }

        #endregion

        #region RemoveOrder
        public static void RemoveOrder(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *No* you want to delete:");
            string removeStr = Console.ReadLine();
            int remove;

            while (!int.TryParse(removeStr, out remove) || remove < 0)
            {
                Console.WriteLine("Error! Enter the *No* correctly!");
                removeStr = Console.ReadLine();
            }

            restaurantManager.RemoveOrder(remove);
        }

        #endregion

        #region ShowAllOrder
        public static void ShowAllOrder(ref RestaurantManager restaurantManager)
        {
            restaurantManager.GetOrders();

        }

        #endregion

        #region ShowOrderByDateInterval
        public static void ShowOrderByDateInterval(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter *First* date");
            DateTime datefirstTime = Convert.ToDateTime(Console.ReadLine());
          

            Console.WriteLine("Enter *Second* date");
            DateTime datesecondTime = Convert.ToDateTime(Console.ReadLine());


            List<Order> ordersDate = restaurantManager.GetOrdersByDatesInterval(datefirstTime, datesecondTime);
            if (ordersDate==null)
            {
                Console.WriteLine("There are no orders in this date range");
            }
            else
            {
                foreach (var orderItem in ordersDate)
                {
                    int totalCount = 0;
                    foreach (var item in orderItem.OrderItems)
                    {
                        totalCount += item.Count;
                    }
                    Console.WriteLine($"No: {orderItem.No}\nTotal Amount: {orderItem.TotalAmount}\nDate: {orderItem.Date}\nCount: {totalCount}");
                }

            }
            


        }

        #endregion

        #region ShowOrdersPriceInterval
        public static void ShowOrdersPriceInterval(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *First* price:");
            string firstPriceStr = Console.ReadLine();
            double firstPrice;

            while (!double.TryParse(firstPriceStr, out firstPrice) || firstPrice <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                firstPriceStr = Console.ReadLine();
            }
            Console.WriteLine("Enter the second *Price*:");

            string secondPriceStr = Console.ReadLine();
            double secondPrice;

            while (!double.TryParse(secondPriceStr, out secondPrice) || secondPrice <= 0)
            {
                Console.WriteLine("Error! Enter the *Price* correctly!");
                secondPriceStr = Console.ReadLine();
            }
            List<Order> orderPrice = restaurantManager.GetOrdersByPriceInterval(firstPrice, secondPrice);
            foreach (var orderItem in orderPrice)
            {
                int totalCount = 0;
                foreach (var item in orderItem.OrderItems)
                {
                    totalCount += item.Count;
                }
                Console.WriteLine($"No: {orderItem.No}\nTotal Amount: {orderItem.TotalAmount}\nDate: {orderItem.Date}\nCount: {totalCount}");
            }

        }

        #endregion

        #region ShowOrderByDate
        public static void ShowOrderByDate(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the *Date*:");
            DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
            restaurantManager.GetOrderByDate(dateTime);
            foreach (var orderItem in restaurantManager.GetOrderByDate(dateTime))
            {
                int totalCount = 0;
                foreach (var item in orderItem.OrderItems)
                {
                    totalCount += item.Count;
                }
                Console.WriteLine($"No: {orderItem.No}\nTotal Amount: {orderItem.TotalAmount}\nDate: {orderItem.Date}\nCount: {totalCount}");
            }
        }
        #endregion

        #region ShowOrderByNo
        public static void ShowOrderByNo(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Enter the order *Number*:");
            string noStr = Console.ReadLine();
            int no;

            while (!int.TryParse(noStr, out no) || no < 0)
            {
                Console.WriteLine("Error! Enter the *Number* correctly!");
                noStr = Console.ReadLine();
            }


            restaurantManager.GetOrderByNo(no);
        }

        #endregion

        #endregion

    }
}
