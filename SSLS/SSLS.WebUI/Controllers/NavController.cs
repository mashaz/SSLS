using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.WebUI.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        private IBooksReopository repository;
        public NavController(IBooksReopository bookRepository)
        {
            this.repository = bookRepository;
        }
        public PartialViewResult Menu(int categoryId = 0)
        {
            ViewBag.CurrentCategoryId = categoryId;
            IEnumerable<Category> categories = repository.Categories.ToList();
            return PartialView(categories);
        }
       
    }
}