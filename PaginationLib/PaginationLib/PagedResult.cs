using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginationLib
{
    /// <summary>
    /// Contains a wrapper for collections that support pagination
    /// </summary>
    public class PagedResult<T>
    {
        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
        public int TotalItems { get; }
        public int PageCount => (int)Math.Ceiling(decimal.Divide(TotalItems, ItemsPerPage));
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Constructor for creating PagedResult<T>
        /// </summary>
        /// <params "items">A subset of a larger collection</params>
        /// <params "currentPageNumber">The current page number representing the current subset of items</params>
        /// <params "itemsPerPage">The size of the current subset of items </params>
        /// <params "totalItems">The total number of items in the larger collection</params>
        public PagedResult(
            IEnumerable<T> items,
            int currentPageNumber,
            int itemsPerPage,
            int totalItems)
        {
            Items = items;
            CurrentPage = currentPageNumber;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
        }
    }

    /// <summary>
    /// Extension methods to extend LINQ and convert to PagedResult<T> instances
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Extension method to IEnumerable<T> to add pagination then produce an instance of PagedResult<T>
        /// </summary>
        /// <params "enumerable">The enumerable being extended upon</params>
        /// <params "currentPageNumber">This indicates the start location in IEnumerable<T> to begin the offset based on the itemsPerPage</params>
        /// <params "itemsPerPage">This indicates the size of the collection returned from IEnumerable<T></params>
        public static PagedResult<T> ToPagedResult<T>(
            this IEnumerable<T> enumerable,
            int currentPageNumber,
            int itemsPerPage)
        {
            var paginatedEnumerable = enumerable
                .Paginate(currentPageNumber, itemsPerPage);

            // Potential unexpected enumeration may occur here for caller
            // paginatedEnumerable will await it's state machine to be called as it's reference is just stored within PagedResult
            // However, the enumerable.Count() will enumerate in the current state when this method is called.
            // Potential fixes:
            // Would this serve better as extension to a collection such as Array<T> rather than IEnumerable<T>? We could also just pull .Length rather than executing .Count()
            return new PagedResult<T>(
                paginatedEnumerable,
                currentPageNumber,
                itemsPerPage,
                enumerable.Count());
        }

        /// <summary>
        /// Extension method to IEnumerable<T> to add paginatination
        /// </summary>
        /// <params "enumerable">The enumerable being extended upon</params>
        /// <params "currentPageNumber">This indicates the start location in IEnumerable<T> to begin the offset based on the itemsPerPage</params>
        /// <params "itemsPerPage">This indicates the size of the collection returned from IEnumerable<T></params>
         public static IEnumerable<T> Paginate<T>(
            this IEnumerable<T> enumerable,
            int currentPageNumber,
            int itemsPerPage)
        {
            if (currentPageNumber == 0)
            {
                throw new InvalidCurrentPageNumberException(
                    $"Invalid currentPageNumber: {currentPageNumber}. This value must be greater than 0."
                );
            }

            int skip = (currentPageNumber - 1) * itemsPerPage;

            return enumerable
                .Skip(skip)
                .Take(itemsPerPage);
        }
    }

    public class InvalidCurrentPageNumberException : Exception
    {
        public InvalidCurrentPageNumberException(string message) : base(message) { }
    }
}
