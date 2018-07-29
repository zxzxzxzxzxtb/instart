using Instart.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web2
{
    public class ManageControllerBase : Controller
    {
        public LoginUser LoginUser
        {
            get
            {
                return CookieData.CurrentUser;
            }
        }

        public IList<IDisposable> DisposableObjects { get; private set; }

        public ManageControllerBase()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj)
        {
            var disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IDisposable obj in this.DisposableObjects)
                {
                    if (obj != null)
                    {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }

        protected JsonResult Success(string msg = null, object data = null)
        {
            return new JsonResult
            {
                Data = new ResultBase
                {
                    success = true,
                    message = msg,
                    data = data
                }
            };
        }

        protected JsonResult Error(string msg = null, object data = null)
        {
            return new JsonResult
            {
                Data = new ResultBase
                {
                    success = false,
                    message = msg,
                    data = data
                }
            };
        }
    }
}