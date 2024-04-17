
using Microsoft.AspNetCore.Mvc;


namespace LR10.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, DateTime consultationDate, string product)
        {
            // Перевірка, чи всі поля заповнені
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || consultationDate == default)
            {
                ViewBag.ErrorMessage = "All fields are required.";
                return View("Index");
            }

            // Перевірка правильності формату email
            if (!IsValidEmail(email))
            {
                ViewBag.ErrorMessage = "Invalid email format.";
                return View("Index");
            }

            // Перевірка, чи дата консультації в майбутньому
            if (consultationDate.Date <= DateTime.Today)
            {
                ViewBag.ErrorMessage = "Consultation date must be in the future.";
                return View("Index");
            }

            // Перевірка, чи дата консультації не попадає на вихідні
            if (consultationDate.DayOfWeek == DayOfWeek.Saturday || consultationDate.DayOfWeek == DayOfWeek.Sunday)
            {
                ViewBag.ErrorMessage = "Consultation date cannot be on a weekend.";
                return View("Index");
            }

            if (product == "Basics" && consultationDate.DayOfWeek == DayOfWeek.Monday)
            {
                ViewBag.ErrorMessage = "Consultation for the Basics product cannot be on Monday.";
                return View("Index");
            }

            // Логіка для збереження реєстраційних даних та відправлення повідомлення про успішну реєстрацію

            // Перенаправлення на сторінку успішної реєстрації
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
