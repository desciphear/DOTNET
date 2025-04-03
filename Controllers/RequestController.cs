using Azure;
using Microsoft.AspNetCore.Mvc;
using Loyal.Models;
using Loyal.Data;
namespace Loyal.Controllers
{
    public class RequestController : Controller
    {
        private readonly Requestcontext _context;
        private readonly ILogger<RequestController> _logger;

        public RequestController(Requestcontext context, ILogger<RequestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new request to the database with a status of "Pending".
        /// </summary>
        /// <param name="model">The request model to add.</param>
        /// <returns>Redirects to the Landing page of the Home controller.</returns>
        public IActionResult RequestAdd(Request model)
        {
            try
            {
                model.Status = "Pending";
                _context.Requests.Add(model);
                _context.SaveChanges();
                _logger.LogInformation("Request added successfully for user {Username}", model.Username);
                return RedirectToAction("Landing", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding request for user {Username}", model.Username);
                // Optionally, you can return an error view or message
                return View("Error");
            }
        }

        public IActionResult DeleteRequest(int id)
        {
            try
            {
                var request = _context.Requests.Find(id);
                _context.Requests.Remove(request);
                _context.SaveChanges();
                _logger.LogInformation("Request deleted successfully for user {Username}", request.Username);
                return RedirectToAction("RefusedRequest", "Admin");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting request with ID {ID}", id);
                return View("Error");
            }
        }

        public IActionResult DeleteRequestUser(int id)
        {
            try
            {
                var request = _context.Requests.Find(id);
                _context.Requests.Remove(request);
                _context.SaveChanges();
                _logger.LogInformation("Request deleted successfully for user {Username}", request.Username);
                return RedirectToAction("Refuse", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting request with ID {ID}", id);
                return View("Error");
            }
        }

        /// <summary>
        /// Displays the index page.
        /// </summary>
        /// <returns>The index view.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
