using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class DTOMapper<TEntity, TEntityDTO>
        where TEntity : Entity
        where TEntityDTO : DTO
    {
        private Type _entityType, _dtoType;
        private ConstructorInfo _dtoCtor;

        public DTOMapper()
        {
            _entityType = typeof(TEntity);
            _dtoType = typeof(TEntityDTO);

            _dtoCtor = _dtoType.GetConstructor(new Type[] { _entityType });
        }

        public Object GetDTOForEntity(Entity entity)
        {
            return _dtoCtor.Invoke(new object[] { entity });
        }

        public IEnumerable<Object> GetDTOForEntities(IEnumerable<Entity> entities)
        {
            List<Object> list = new List<Object>();
            foreach (var entity in entities)
            {
                list.Add(GetDTOForEntity(entity));
            }

            return list;
        }
    }
}