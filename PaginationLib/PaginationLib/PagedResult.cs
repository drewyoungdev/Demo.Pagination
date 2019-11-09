using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginationLib
{
    public class PagedResult<T> where T : IEnumerable<T>
    {
        // naming convention is based on UI component library
        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
        public int TotalItems { get; }
        public int PageCount => (int)Math.Ceiling(decimal.Divide(TotalItems, ItemsPerPage));

        public T Items { get; }

        public PagedResult(
            T items,
            int currentPageNumber,
            int itemsPerPage)
        {
            // is converting to list practical? Or should we detect what type is being sent in
            Items = items;
            CurrentPage = currentPageNumber;
            ItemsPerPage = itemsPerPage;
            TotalItems = items.Count();
        }
    }

    public static class PaginationExtensions
    {
         public static IEnumerable<T> Paginate<T>(
            this IEnumerable<T> enumerable,
            int currentPageNumber,
            int itemsPerPage)
        {
            if (currentPageNumber == 0)
            {
                // current page number must start at at least 1
                throw new InvalidCurrentPageNumberException(
                    $"Invalid currentPageNumber: {currentPageNumber} when attempting to paginate. This value must be greater than 0."
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
        public InvalidCurrentPageNumberException(string message) : base(message)
        {
        }
    }
}
