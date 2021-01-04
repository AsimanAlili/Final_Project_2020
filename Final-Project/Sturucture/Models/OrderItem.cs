using Final_Project.Sturucture.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Models
{
    public class OrderItem
    {
        #region OrderItem
        private int _count;
        public MenuItem MenuItem { get; set; }
        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value < 0)
                {
                    throw new OrderItemModelInvalidException("Count", "Count  can not be less than zero!");
                }
                else
                {
                    this._count = value;
                }


            }
        }

        public OrderItem(MenuItem menuItem, int count)
        {
            this.MenuItem = menuItem;
            this.Count = count;
        }
        #endregion

    }
}
