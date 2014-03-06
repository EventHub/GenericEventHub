using GenericEventHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GenericEventHub.DTOs
{
    public class DTOMapper
    {

        public DTOMapper()
        {
        }

        public TEntityDTO GetDTOForEntity<TEntity, TEntityDTO>(TEntity entity)
        {
            var dtoType = typeof(TEntityDTO);
            var entityType = typeof(TEntity);

            var dtoCtor = dtoType.GetConstructor(new Type[] { entityType });
            return (TEntityDTO)dtoCtor.Invoke(new object[] { entity });
        }

        public IEnumerable<TEntityDTO> GetDTOsForEntities<TEntity, TEntityDTO>(IEnumerable<TEntity> entities)
        {
            var dtoType = typeof(TEntityDTO);
            var entityType = typeof(TEntity);
            var dtoCtor = dtoType.GetConstructor(new Type[] { entityType });
            var list = new List<TEntityDTO>();
            foreach (var entity in entities)
            {
                list.Add((TEntityDTO)dtoCtor.Invoke(new object[] { entity }));
            }
            return list;
        }
    }
}