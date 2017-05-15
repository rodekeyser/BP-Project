using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBusinessLayer.BusinessLogicObjects
{
    public class ValidationException: Exception
    {
        public string PropertyName { get; set; }
        //public ValidationException() { }
        public ValidationException(string message, string propertyName): base(message)
        {
            this.PropertyName = propertyName;
        }
    }
}
