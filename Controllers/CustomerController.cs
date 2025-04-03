using Loyal.Models;
using Loyal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Loyal.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerContext _customcontext;
        private readonly Requestcontext _rcontext;
        private readonly ILogger<HomeController> _logger;
        [ActivatorUtilitiesConstructor]
        public CustomerController(CustomerContext customercontext, Requestcontext requestcontext, ILogger<HomeController> logger)
        {
            _customcontext = customercontext;
            _rcontext = requestcontext;
            _logger = logger;
        }

        public IActionResult Login()
        {   HttpContext.Session.Remove("Username");
            return View();
        }

        public IActionResult ValidateUser(Customer model)
        {

            var user = _customcontext.Customers.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                
                HttpContext.Session.SetString("Username", $"{model.Username}");
                return RedirectToAction("CustomerLanding");
            }
            else
            {
                TempData["Loginf"] = "Invalid Username or Password";
                return View("Login");
            }
        }

        public IActionResult OfferDetails(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("Username"))
            {   
                return RedirectToAction("Login");
            }
            else
            {
                try
                {
                    if (id != null)
                    {   HttpContext.Session.SetInt32("Id", (int)id);
                        var dat = _rcontext.Requests.Find(id);
                        return View(dat);
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching details for request ID: {Id}", id);
                    return View("Error");
                }
            }
        }

        public IActionResult qrcode()
        {
            if (!HttpContext.Session.Keys.Contains("Username"))
            {
                return RedirectToAction("Login");
            }
            else
            {
                var id = HttpContext.Session.GetInt32("Id");
                var dat = _rcontext.Requests.Find(id);
                return View(dat);
            }
        }

        public IActionResult CustomerLanding()
        {
            if (!HttpContext.Session.Keys.Contains("Username"))
            {
                return RedirectToAction("Login");
            }
            else
            {   var dat = _rcontext.Requests.Where(e => e.Status == "Approved").ToList();
                dat.Reverse();
                return View(dat);
            }
        }

        public IActionResult Used()
        {
            if (!HttpContext.Session.Keys.Contains("Username"))
            {
                return Login();
            }
            else
            {
                var dat = _customcontext.Customers.ToList();
                dat.Reverse();
                return View(dat);
            }
        }
    }
}
