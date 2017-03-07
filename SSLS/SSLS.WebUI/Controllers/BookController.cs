using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSLS.Domain.Concrete;
using SSLS.Domain.Abstract;
using SSLS.WebUI.Models;

namespace SSLS.WebUI.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private IBooksReopository repository;
        public int pageSize = 6;
        public BookController(IBooksReopository bookReopository)
        {
            this.repository = bookReopository;
        }
        public ActionResult List(int categoryId=0,int page=1)
        {
            IQueryable<Book> bookList = repository.Books.Where(c=>c.Status == "1");
            if (categoryId > 0)
            {
                bookList = repository.Books.Where(p => p.CategoryId == categoryId);
            }
            BooksListViewModel viewModel = new BooksListViewModel
            {

                Books = bookList
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                pagingInfo = new PagingInfo { 
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = bookList.Count()
            
                }
            };
            return View(viewModel);
        }
    }
}