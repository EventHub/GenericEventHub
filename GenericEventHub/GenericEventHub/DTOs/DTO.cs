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
                //if (property.PropertyType.FullName.Contains("DTO"))
                //    continue;
                var xProperty = xType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance);
                var propertyType = property.PropertyType;
                if (xProperty != null && xProperty.CanRead) {
                    var xValue = xProperty.GetValue(x, null);
                    if (propertyType.IsGenericType
                            && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>) 
                            && IsDTO(propertyType)) {
                        //xValue = ResolveListDTO(xValue, propertyType);
                                continue;
                    }
                    else if (IsDTO(propertyType))
                    {
                        xValue = ResolveDTO(xValue, propertyType);
                    }
                    property.SetValue(this, xValue, null);
                }
            }
        }

        public bool IsDTO(Type type)
        {
            return type.FullName.Contains("DTO");
        }

        public Object ResolveDTO(Object obj, Type dto)
        {
            var mapper = CreateMapper(obj.GetType(), dto);
            Object[] parameters = { obj };
            return mapper.GetType().GetMethod("GetDTOForEntity").Invoke(mapper, parameters);
        }

        // Need to loop over list and resolve each dto.
        //public Object ResolveListDTO(Object obj, Type dto)
        //{
        //    var mapper = CreateMapper(obj.GetType().GetGenericArguments()[0], 
        //        dto.GetGenericArguments()[0]);
        //    Object[] parameters = { obj };
        //    var listObjs = mapper.GetType().GetMethod("GetDTOForEntities").Invoke(mapper, parameters);
        //    return listObjs;
        //}

        public Object CreateMapper(Type obj, Type dto)
        {
            var mapperType = typeof(DTOMapper<,>);
            Type[] typeArgs = { obj , dto };
            var mapperMake = mapperType.MakeGenericType(typeArgs);
            Object mapper = Activator.CreateInstance(mapperMake);
            return mapper;
        }
    }
}