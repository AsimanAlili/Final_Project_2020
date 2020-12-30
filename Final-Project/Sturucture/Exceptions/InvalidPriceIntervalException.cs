using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Sturucture.Exceptions
{
    class InvalidPriceIntervalException:Exception
    {
        private string _message;
        public string InvalidPropName;

        public InvalidPriceIntervalException(string propName, string message = null)
        {
            this._message = message;
            this.InvalidPropName = propName;
        }

        public override string Message => _message;
    }
}
