using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSLS.Domain.Concrete;

namespace SSLS.WebUI.Models
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public int CurrentCategoryId { get; set; }
        
    }
}