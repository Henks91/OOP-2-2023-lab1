using System;
using System.Collections.Generic;
using System.Linq;

namespace Datalager
{
    /// <summary>
    ///  Generic repository class.
    /// </summary>
    public class Repository<T>
        where T : class
    {
        /// <summary>
        ///  Add a new entity to the Table.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            table.Add(entity);
        }
        /// <summary>
        ///  Remove an entity from the Table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true if removed and false otherwise.</returns>
        public bool Remove(T entity)
        {
            return table.Remove(entity);
        }
        /// <summary>
        ///  Find a set of entities that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return table.Where(predicate);
        }
        /// <summary>
        ///  Find the first entity that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return table.FirstOrDefault(predicate);
        }
        /// <summary>
        ///  Is this repository empty?
        /// </summary>
        /// <returns>true is it is empty, false otherwise.</returns>
        public bool IsEmpty()
        {
            return table.Count == 0;
        }
        /// <summary>
        ///  Count the entities in the Table.
        /// </summary>
        /// <returns>the number of entities.</returns>
        public int Count()
        {
            return table.Count();
        }
        internal Repository()
        {
            if (table == null)
            {
                table = new List<T>();
            }
        }
        // This is a bit strange but I don't want multiple lists of the class T.
        // NOTE: This is very bad if you use multiple threads.
        private static IList<T> table;
    }





}
