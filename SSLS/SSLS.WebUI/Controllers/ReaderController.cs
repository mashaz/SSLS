using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;
using SSLS.WebUI.Models;

namespace SSLS.WebUI.Controllers
{
    public class ReaderController : Controller
    {
        // GET: Reader
        private IBooksReopository repository;
        private IOrderProcessor orderProcessor;
        public ReaderController(IBooksReopository bookRepository,IOrderProcessor proc)
        {
            this.repository = bookRepository;
            this.orderProcessor = proc;
        }
        //public ActionResult Register()
        //{
        //   return 
        //}
        public ActionResult Login()
        {
            return View();
        }
        public PartialViewResult Summary(Reader reader)
        {

            return PartialView(reader);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                    Reader readerEntry = repository.Readers.FirstOrDefault(c =>
                         c.Id == model.Id &&
                         c.Password == model.Password);
                    if (readerEntry != null)
                    {
                        HttpContext.Session["Reader"] = readerEntry;
                        return Redirect(returnUrl ?? Url.Action("List", "Book"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户名或密码不正确");
                        return View();
                    }

            }
            else { return View(); }
        }
        public ActionResult LogOut()
        {
            Session["Reader"] = null;
            return RedirectToAction("List","Book");
        }

        public ViewResult ReaderInfo(Reader reader)
        {
            Reader readers= repository.Readers.FirstOrDefault(c => c.Id == reader.Id );
            return View(readers);
        }
        public ViewResult ChangePwd(Reader reader)
        {
            Reader readers = repository.Readers.FirstOrDefault(c => c.Id == reader.Id);
            return View(readers);
        }
        public void ConfirmChange(string pwd,Reader reader)
        {

            if (ModelState.IsValid)
            {
                orderProcessor.ChangePwdOrder(pwd,reader);
              
            }
           
        }
        public ViewResult Recharge(Reader reader)
        {
            Reader readers = repository.Readers.FirstOrDefault(c => c.Id == reader.Id);
            return View(readers);
        }
        public void RechargeAction(string money,Reader reader)
        {
            orderProcessor.ChangeBalanceOrder(money, reader);
           
        }
        public ActionResult Register()
        {
            return View();
        }
        public void RegisterAction(string username, string pwd)
        {
            orderProcessor.RegisterAccount(username, pwd);
        }
        
    }
}