using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.DAL.Models
{
    public class PageViewModelDTO
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModelDTO(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
