using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSLS.Domain.Abstract;
using SSLS.Domain.Concrete;

namespace SSLS.WebUI.Infrastructure.Binders
{
    public class ReaderModelBinder :IModelBinder
    {
        private const string sessionKey = "Reader";
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            Reader reader = null;
            if (controllerContext.HttpContext.Session != null)
            {
                reader = controllerContext.HttpContext.Session[sessionKey] as Reader;
            }
            if (reader == null)
            {
                reader = new Reader();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = reader;
                }
            }
            return reader;
        }
    }
}