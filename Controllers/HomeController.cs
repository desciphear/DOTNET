using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Loyal.Models;
using Loyal.Data;
using Microsoft.EntityFrameworkCore;

namespace Loyal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserContext _context;
    private readonly Requestcontext _rcontext;

    [ActivatorUtilitiesConstructor]
    public HomeController(ILogger<HomeController> logger, UserContext context, Requestcontext rcontext)
    {
        _logger = logger;
        _context = context;
        _rcontext = rcontext;
    }

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="model">The user model to add.</param>
    /// <returns>Redirects to the Login page if successful, otherwise to the Register page with an error message.</returns>
    public IActionResult Adduser(User model)
    {
        try
        {
            var em = _context.Users.FirstOrDefault(e => e.Email == model.Email);
            var un = _context.Users.FirstOrDefault(e => e.Username == model.Username);
            var pan = _context.Users.FirstOrDefault(e => e.PanCard == model.PanCard);
            var adhaar = _context.Users.FirstOrDefault(e => e.AdhaarCard == model.AdhaarCard);

            if (em == null && un == null && pan == null && adhaar == null)
            {
                model.Status = "InActive";
                _context.Users.Add(model);
                _context.SaveChanges();
                _logger.LogInformation("User added successfully: {Username}", model.Username);
                return RedirectToAction("Login");
            }
            else if (em != null && un != null && adhaar != null && pan == null)
            {
                TempData["UserExists"] = "User already exists.";
                return RedirectToAction("Register");
            }
            else if (em != null || un != null)
            {
                TempData["UserExists"] = "Email and/or Username already exists.";
                return RedirectToAction("Register");
            }
            else if (adhaar != null && pan != null)
            {
                TempData["UserExists"] = "Adhaar Card and Pan Card already exists.";
                return RedirectToAction("Register");
            }
            else if (pan != null)
            {
                TempData["UserExists"] = "Pan Card already exists.";
                return RedirectToAction("Register");
            }
            else
            {
                TempData["UserExists"] = "Adhaar Card already exists.";
                return RedirectToAction("Register");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding user: {Username}", model.Username);
            return View("Error");
        }
    }

    public IActionResult Approved()
    {
        if (!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else { 
            try
            {
                var username = HttpContext.Session.GetString("Username");
                var dat = _rcontext.Requests
                                          .Where(p => p.Status == "Approved" && p.Username == username)
                                          .ToList();
                dat.Reverse();
                return View(dat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching approved requests for user: {Username}", HttpContext.Session.GetString("Username"));
                return View("Error");
            }
        }
    }

    public IActionResult Details(int? id)
    {   if (!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else{
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
            }}
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Landing()
    {
        if (!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else
            {try
            {
                var username = HttpContext.Session.GetString("Username");
                TempData["Requests"] = _rcontext.Requests.Count(e => e.Username == username);
                TempData["Pending"] = _rcontext.Requests.Count(e => e.Username == username && e.Status == "Pending");
                TempData["Denied"] = _rcontext.Requests.Count(e => e.Username == username && e.Status == "Denied");
                TempData["Approved"] = _rcontext.Requests.Count(e => e.Username == username && e.Status == "Approved");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching landing data for user: {Username}", HttpContext.Session.GetString("Username"));
                return View("Error");
            }
        }
    }

    public IActionResult Login()
    {   HttpContext.Session.Remove("Username");
        return View();
    }

    public IActionResult Pending()
    {
        if (!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else
           { try
            {
                var username = HttpContext.Session.GetString("Username");
                var dat = _rcontext.Requests
                                          .Where(p => p.Status == "Pending" && p.Username == username)
                                          .ToList();
                dat.Reverse();
                return View(dat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching pending requests for user: {Username}", HttpContext.Session.GetString("Username"));
                return View("Error");
            }
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Refuse()
    { if(!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                var dat = _rcontext.Requests
                                          .Where(p => p.Status == "Denied" && p.Username == username)
                                          .ToList();
                dat.Reverse();
                return View(dat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching refuse data for user: {Username}", HttpContext.Session.GetString("Username"));
                return View("Error");
            }
        }
        
    }

    public IActionResult RefusedDetails(int? id)
    {
        if (!HttpContext.Session.Keys.Contains("Username"))
        {
            return RedirectToAction("Login");
        }
        else{
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

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Requests()
    {   if(!HttpContext.Session.Keys.Contains("Username"))
            { return RedirectToAction("Login"); }
        else
            {TempData["Username"] = HttpContext.Session.GetString("Username");
            return View();
        }
    }

    public IActionResult ULanding()
    {
        try
        {
            var dat = _rcontext.Requests.Where(e => e.Username == HttpContext.Session.GetString("Username")).ToList();
            var reason = dat.Select(e => e.Reason).FirstOrDefault();
            TempData["Reason"] = reason;
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching ULanding data for user: {Username}", HttpContext.Session.GetString("Username"));
            return View("Error");
        }
    }

    public IActionResult ValidateLogin(User model)
    {  
        try
        {
            var user = _context.Users.FirstOrDefault(s => s.Username == model.Username && s.PasswordHash == model.PasswordHash);
            if (user != null)
            {
                if (user.Status != "Activated")
                {
                    TempData["Login"] = "User is not active";
                    return RedirectToAction("ULanding");
                }
                else
                {
                    HttpContext.Session.SetString("Username", $"{model.Username}");
                    return RedirectToAction("Landing");
                }
            }
            else
            {
                TempData["Loginf"] = "Invalid Credentials";
                return RedirectToAction("Login");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating login for user: {Username}", model.Username);
            return View("Error");
        }
    }
}
