using Loyal.Data;
using Loyal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Loyal.Controllers
{   

   
public class AdminController : Controller
    {
        private readonly AdminContext _context;
        private readonly UserContext _ucontext;
        private readonly Requestcontext _rcontext;
        private readonly ILogger<AdminController> _logger;

        [ActivatorUtilitiesConstructor]
        public AdminController(AdminContext context, UserContext ucontext, Requestcontext rcontext, ILogger<AdminController> logger)
        {
            _ucontext = ucontext;
            _context = context;
            _rcontext = rcontext;
            _logger = logger;
        }

        public IActionResult AdminLanding()
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                TempData["Pending"] = _rcontext.Requests.Count(e => e.Status == "Pending");
                TempData["Approved"] = _rcontext.Requests.Count(e => e.Status == "Approved");
                TempData["DeniedR"] = _rcontext.Requests.Count(e => e.Status == "Denied");
                TempData["Requests"] = _rcontext.Requests.Count(e => 1 == 1);
                TempData["Clients"] = _ucontext.Users.Count(e => 1 == 1);
                TempData["InActive"] = _ucontext.Users.Count(e => e.Status == "InActive");
                TempData["Denied"] = _ucontext.Users.Count(e => e.Status == "Denied");
                TempData["Activate"] = _ucontext.Users.Count(e => e.Status == "Activated");
                return View();
            }
        }

        public IActionResult AdminPage()
        {
            if (HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                HttpContext.Session.Remove("AdminUsername");
            }
            return View();
        }

        public IActionResult ApproveRequest(Request model)
        {
            try
            {
                if (model.Status != "Denied")
                {
                    model.Reason = "";
                    _rcontext.Requests.Update(model);
                    _rcontext.SaveChanges();
                }
                else
                {
                    _rcontext.Requests.Update(model);
                    _rcontext.SaveChanges();
                }
                return RedirectToAction("ViewRequest");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving request for user: {Username}", model.Username);
                return View("Error");
            }
        }

        public IActionResult ApprovedRequest()
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    var dat = _rcontext.Requests.Where(s => s.Status == "Approved").ToList();
                    dat.Reverse();
                    return View(dat);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching approved requests");
                    return View("Error");
                }
            }
        }

        public IActionResult Details(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    if (id != null)
                    {
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

        public IActionResult PartnerDetails()
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    var data = _ucontext.Users.ToList();
                    data.Reverse();
                    return View(data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching partner details");
                    return View("Error");
                }
            }
        }

        public IActionResult RefusedDetails(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    if (id != null)
                    {
                        var dat = _rcontext.Requests.Find(id);
                        return View(dat);
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching refused details for request ID: {Id}", id);
                    return View("Error");
                }
            }
        }

        public IActionResult RefusedRequest()
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    var dat = _rcontext.Requests.Where(s => s.Status == "Denied").ToList();
                    return View(dat);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching refused requests");
                    return View("Error");
                }
            }
        }

        public IActionResult RequestDetails(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    if (id != null)
                    {
                        var dat = _rcontext.Requests.Find(id);
                        return View(dat);
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching request details for request ID: {Id}", id);
                    return View("Error");
                }
            }
        }

        public IActionResult UpdateStatus(User model)
        {
            try
            {
                if (model.Status != "Denied")
                {
                    model.Reason = "";
                    _ucontext.Users.Update(model);
                    _ucontext.SaveChanges();
                }
                else
                {
                    _ucontext.Users.Update(model);
                    _ucontext.SaveChanges();
                }
                return RedirectToAction("PartnerDetails");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for user: {Username}", model.Username);
                return View("Error");
            }
        }

        public IActionResult ValidateUser(AdminCred model)
        {
            try
            {
                var user = _context.AdminCreds.FirstOrDefault(s => s.Username == model.Username && s.Password == model.Password);
                HttpContext.Session.SetString("AdminUsername", $"{model.Username}");

                if (user != null)
                {
                    return RedirectToAction("AdminLanding");
                }
                else
                {
                    TempData["Login"] = "Invalid Credentials";
                    return RedirectToAction("AdminPage");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating admin user: {Username}", model.Username);
                return View("Error");
            }
        }

        public IActionResult ViewPartner(int? id)
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    if (id != null)
                    {
                        var dat = _ucontext.Users.Find(id);
                        return View(dat);
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching partner details for user ID: {Id}", id);
                    return View("Error");
                }
            }
        }

        public IActionResult ViewRequest()
        {
            if (!HttpContext.Session.Keys.Contains("AdminUsername"))
            {
                return RedirectToAction("AdminPage");
            }
            else
            {
                try
                {
                    var data = _rcontext.Requests.Where(e => e.Status == "Pending").ToList();
                    data.Reverse();
                    return View(data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching pending requests");
                    return View("Error");
                }
            }
        }
    }
}
