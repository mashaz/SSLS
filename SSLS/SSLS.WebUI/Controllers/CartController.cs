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
    public class CartController : Controller
    {
        // GET: Cart
        private IBooksReopository respository;
        private IOrderProcessor orderProcessor;
        public int pageSize = 6;
        public CartController(IBooksReopository bookRespository, IOrderProcessor proc)
        {
            this.respository = bookRespository;
            this.orderProcessor = proc;
        }
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public RedirectToRouteResult AddToCart(Cart cart, int id, string returnUrl)
        {
            Book book = respository.Books.FirstOrDefault(p => p.Id == id);
            if (book != null)
            {
                cart.AddItem(book, 1);

            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFormCart(Cart cart, int id, string returnUrl)
        {
            Book book = respository.Books.FirstOrDefault(p => p.Id == id);
            if (book != null)
            {
                cart.RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary()
        {
            return PartialView();
        }
        public ViewResult Index(string returnUrl)
        {

            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }
        public ViewResult Return(Reader reader)
        {
            {
                ICollection<Borrow> borrows = respository.Borrows.Where(b => b.ReaderId == reader.Id && b.Renew !=true).ToList();
                int i = borrows.Count();
            
                return View(borrows);
            }
        }

     
        public void returnAction(int id,Reader reader)
        {
             if (ModelState.IsValid)
            {
             //reader = respository.Readers.FirstOrDefault(c => c.Id == reader.Id);
             orderProcessor.ProcessReturn(id,reader);
            
            }
             
           
        }
        public ActionResult completed()
        {
            return View();
        }
     public void ContinueBorrow(int id,Reader reader)
        {
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessContinue(id, reader);
                
            }
          
        }
     public ActionResult History(Reader reader)
     {

         ICollection<Borrow> borrows = respository.Borrows.Where(b => b.ReaderId == reader.Id && b.Renew == true).ToList();
         int i = borrows.Count();

         return View(borrows);

     }

        public ViewResult Checkout(Cart cart, Reader reader)
        {
            if (reader.Id == 0)//临时
            {
                //  customer = respository.Customers.FirstOrDefault(c => c.Id == 1);
                ModelState.AddModelError("", "对不起，请先登陆");

            }
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "对不起，读书列表是空的！");

            }
            if (ModelState.IsValid)
            {
                if (reader.Id == 0)
                {
                    reader = respository.Readers.FirstOrDefault(c => c.Id == 1);
                }
                orderProcessor.ProcessOrder(cart,reader);
                cart.Clear();
                return View("completed");
            }
            else
            {
                return View("sorry");
            }
        }
    }
}