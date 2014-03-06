using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace GenericEventHub.DTOs
{
    public class DTO
    {
        private DTOMapper _mapper;

        public DTO(Object x)
        {
            _mapper = new DTOMapper();
            
            var dtoMethod = _mapper.GetType().GetMethod("GetDTOForEntity");
            var dtosMethod = _mapper.GetType().GetMethod("GetDTOsForEntities");
            var myProperties = this.GetType().GetProperties();
            var xType = x.GetType();

            foreach (var property in myProperties)
            {
                // Skip other DTOs for name, it will be handled later
                //if (property.PropertyType.FullName.Contains("DTO"))
                //    continue;
                var xProperty = xType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);
                var propertyType = property.PropertyType;
                if (xProperty != null && xProperty.CanRead) {
                    var xValue = xProperty.GetValue(x, null);
                    if (propertyType.IsGenericType
                            && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>) 
                            && IsDTO(propertyType)) {
                        var generic = dtosMethod.MakeGenericMethod(xValue.GetType().GetGenericArguments()[0],
                            propertyType.GetGenericArguments()[0]);
                        xValue = generic.Invoke(_mapper, new object[] { xValue }); 
                        //continue;
                    }
                    else if (IsDTO(propertyType))
                    {
                        var generic = dtoMethod.MakeGenericMethod(xValue.GetType(), propertyType);
                        xValue = generic.Invoke(_mapper, new object[] {xValue});
                    }
                    property.SetValue(this, xValue, null);
                }
            }
        }

        public bool IsDTO(Type type)
        {
            return type.FullName.Contains("DTO");
        }
    }
}