using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCodeBase.Web.Controllers
{
    public class VueTestController : Controller
    {
        private readonly IConfiguration _config;

        public VueTestController(IConfiguration config)
        {
            _config = config;
        }

        // GET: VueTest
        public ActionResult Index()
        {
            return View();
        }
    }
}