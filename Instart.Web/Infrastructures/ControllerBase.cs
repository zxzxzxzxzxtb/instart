using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instart.Web
{
    public class ControllerBase : Controller
    {
        public IList<IDisposable> DisposableObjects { get; private set; }

        public ControllerBase() {
            this.DisposableObjects = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj) {
            var disposable = obj as IDisposable;
            if (disposable != null) {
                this.DisposableObjects.Add(disposable);
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                foreach(IDisposable obj in this.DisposableObjects) {
                    if(obj != null) {
                        obj.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}