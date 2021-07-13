using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wmsds.Entities
{
    public class WmsdsResponse<T>
    {
        public EnumStatus ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T DataObject { get; set; }
        public List<T> Collections { get; set; }
        public Pager Pager { get; set; }

        public int TotalRecords { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }

        //Now added
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

    public enum EnumStatus
    {
        Success = 1000,
        AlreadyExist = 1001,        
        Failed = 1002,
        Error = 1003,
        InternalServer = 1004,
        ValidationFailed = 1005,
        NoSeatVacant = 1006,
        NotFound = 1007,
        RequiredField = 1008,
        EmptyObject=1009
    }
}
