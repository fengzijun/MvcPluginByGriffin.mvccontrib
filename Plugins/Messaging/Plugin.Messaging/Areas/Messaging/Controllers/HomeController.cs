﻿using System.Web.Mvc;
using Plugin.Messaging.Services;

namespace Plugin.Messaging.Areas.Messaging.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHelloService _service;

        public HomeController(IHelloService service)
        {
            _service = service;
        }

        //
        // GET: /Messaging/Home/

        public ActionResult Index()
        {
            var model = _service.GetMessage();
            return View((object)model);
        }

    }
}
