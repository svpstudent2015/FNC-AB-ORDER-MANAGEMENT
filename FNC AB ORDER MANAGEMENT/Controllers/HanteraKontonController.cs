using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Globalization;
using System.Security.AccessControl;
using System.Web.Http;

namespace FNC_AB_ORDER_MANAGEMENT.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class HanteraKontonController : Controller
    {
        public ActionResult HanteraKonton()
        {
            return View();
        }
    }
}