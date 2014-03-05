using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace GenericEventHub.DTOs
{
    public class DTO
    {
        public DTO(Object x)
        {
            var myProperties = this.GetType().GetProperties();
            var xType = x.GetType();
            foreach (var property in myProperties)
            {
                // Skip other DTOs for name, it will be handled later
                if (property.PropertyType.FullName.Contains("DTO"))
                    continue;
                var xProperty = xType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);
                if (xProperty != null && xProperty.CanRead) {
                    var xValue = xProperty.GetValue(x, null);
                    property.SetValue(this, xValue, null);
                }
            }
        }
    }
}