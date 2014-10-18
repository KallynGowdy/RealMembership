using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMembership.CollectionHelpers
{
    /// <summary>
    /// Defines a class that wraps a collection of a more derived type in order to provide access to a collection of the base type.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    /// <typeparam name="T">The type of the collection that should be exposed.</typeparam>
    public sealed class DerivedCollectionWrapper<TDerived, T> : ICollection<T>
        where TDerived : T
    {
        ICollection<TDerived> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DerivedCollectionWrapper{TDerived, T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <exception cref="ArgumentNullException">collection</exception>
        public DerivedCollectionWrapper(ICollection<TDerived> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            this.collection = collection;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public int Count
        {
            get
            {
                return collection.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return collection.IsReadOnly;
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            collection.Add((TDerived)item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        public void Clear()
        {
            collection.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return collection.Contains((TDerived)item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return collection.Cast<T>().GetEnumerator();
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return collection.Remove((TDerived)item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}
