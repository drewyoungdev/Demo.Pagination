using System;
using System.Collections.Generic;
using System.Linq;

namespace PaginationLib
{
    public class PagedResult<T>
    {
        public int CurrentPage { get; }
        public int ItemsPerPage { get; }
        public int TotalItems { get; }
        public int PageCount => (int)Math.Ceiling(decimal.Divide(TotalItems, ItemsPerPage));
        public IEnumerable<T> Items { get; }

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

    public static class PaginationExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(
            this IEnumerable<T> enumerable,
            int currentPageNumber,
            int itemsPerPage)
        {
            var paginatedEnumerable = enumerable
                .Paginate(currentPageNumber, itemsPerPage);

            return new PagedResult<T>(
                paginatedEnumerable,
                currentPageNumber,
                itemsPerPage,
                enumerable.Count());
        }

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
