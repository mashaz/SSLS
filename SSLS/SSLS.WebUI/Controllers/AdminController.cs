using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSLS.Domain.Concrete;
using SSLS.Domain.Abstract;
using System.IO;
using SSLS.WebUI.Models;

namespace SSLS.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBooksReopository repository;
        public AdminController(IBooksReopository bookRepository)
        {
            this.repository = bookRepository;
            
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Books.ToList());
        }

     

        public ActionResult Edit(Book book)
        {
            string imageFileName = string.Empty;
            //if (modelstate.isvalid)
            //{
                //repository.savebook(book);
                //tempdata["msg"] = string.format("{0} 保存成功", book.name);
               //return redirecttoaction("index");
            //}
            //else
            //{
            IEnumerable<SelectListItem> selectListItem =
            repository.Categories.ToList().Select(
            c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }
            );
            ViewBag.CategoryList = selectListItem;
            return View(book);
            //}


        }
        public ActionResult CreateCategory()
        {
            return View(new Category());
        }
        public ActionResult Create()
        {
            ViewBag.CategoryList = GetCategorySelectList();
            return View("Edit", new Book());
        }
        private IEnumerable<SelectListItem> GetCategorySelectList()
        {
            return repository.Categories.ToList().Select(
                    c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
        }
        //[HttpPost]
        public ActionResult Delete(int id)
        {
            Book deletedProduct = repository.DeleteBook(id);
            if (deletedProduct != null)
            {
                TempData["msg"] = string.Format("“{0}”产品信息删除成功", deletedProduct.Name);

            }
            return RedirectToAction("Index");
        }
        public ActionResult EditCategory()
        {
            return View(repository.Categories.ToList());
        }
        public ActionResult ReaderManage()
        {
            return View(repository.Readers.ToList());
        }
        public ActionResult CreateReader()
        {
            return View(new Category());
        }
        public ActionResult AdminLogin()
        {
            return View("Index","Admin");
        }
      
        //[HttpPost]
        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Reader readerEntry = repository .FirstOrDefault(c =>
        //             c.Id == model.Id &&
        //             c.Password == model.Password);
        //        if (readerEntry != null)
        //        {
        //            HttpContext.Session["Reader"] = readerEntry;

        //            return Redirect(returnUrl ?? Url.Action("List", "Book"));

        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "用户名或密码不正确");
        //            return View();
        //        }
        //    }
        //    else { return View(); }
        //}
        //public ActionResult LogOut()
        //{
        //    Session["Reader"] = null;
        //    return RedirectToAction("List", "Book");
        //}
    }
}