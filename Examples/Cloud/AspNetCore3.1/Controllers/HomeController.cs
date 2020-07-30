using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FiftyOne.GeoLocation.Core.Data;
using FiftyOne.Pipeline.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore31.Controllers
{
    public class HomeController : Controller
    {
        private IFlowDataProvider _flow;

        public HomeController(IFlowDataProvider flow)
        {
            _flow = flow;
        }

        public IActionResult Index()
        {
            var data = _flow.GetFlowData().Get<IGeoData>();
            return View(data);
        }
    }
}
