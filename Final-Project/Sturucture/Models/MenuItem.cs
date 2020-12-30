using Final_Project.Sturucture.Enums;
using Final_Project.Sturucture.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Models
{
    public class MenuItem
    {
        private double _price;
        private string _name;
        private string _no;
        public string No { get { return _no; } }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new MenuModelInvalidException("Name", "Name can not be empty");
                else
                    _name = value;
            }
        }
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value < 0)
                {
                    throw new MenuModelInvalidException("Price", "Price can not be less than zero!");
                }
                else
                {
                    _price = value;
                }
               
            }
        }
        public Category Category { get; set; }

        #region MenuItemCounterAndName
        private static int MenuItemMainCounter { get; set; } = 100;
        private static int MenuItemSoupCounter { get; set; } = 100;
        private static int MenuItemDrinkCounter { get; set; } = 100;
        private static int MenuItemDesertCounter { get; set; } = 100;

        private static string MenuItemMainName { get; set; } = "MA";
        private static string MenuItemSoupName { get; set; } = "SO";
        private static string MenuItemDrinkName { get; set; } = "DR";
        private static string MenuItemDesertName { get; set; } = "DE";

        #endregion
        public MenuItem(string name, double price, Category category)
        {
            this._name = name;
           
            this._price = price;
            this.Category = category;

            #region NoCreation
            string MenuItemMainCounterStr;
            string MenuItemSoupCounterStr;
            string MenuItemDrinkCounterStr;
            string MenuItemDesertCounterStr;


            switch (category)
            {
                case Category.Main:
                    MenuItemMainCounter++;
                    MenuItemMainCounterStr = Convert.ToString(MenuItemMainCounter);
                    _no = MenuItemMainName + MenuItemMainCounterStr;
                    break;
                case Category.Soup:
                    MenuItemSoupCounter++;
                    MenuItemSoupCounterStr = Convert.ToString(MenuItemSoupCounter);
                    _no = MenuItemSoupName + MenuItemSoupCounterStr;
                    break;
                case Category.Desert:
                    MenuItemDesertCounter++;
                    MenuItemDesertCounterStr = Convert.ToString(MenuItemDesertCounter);
                    _no = MenuItemDesertName + MenuItemDesertCounterStr;
                    break;
                case Category.Drink:
                    MenuItemDrinkCounter++;
                    MenuItemDrinkCounterStr = Convert.ToString(MenuItemDrinkCounter);
                    _no = MenuItemDrinkName + MenuItemDrinkCounterStr;
                    break;
                    #endregion

            }
        }
    }
}
