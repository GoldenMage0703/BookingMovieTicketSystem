using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;
using Web.SessionExtensions;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRepository<User> repository;

        public LoginModel(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = await repository.GetUserByUsernamePassword(UserName, Password);

            if (user != null)
            {
                if (user.RoleId == 2)
                {
                    HttpContext.Session.SetComplexData("user", user);
                    HttpContext.Session.SetString("userName", user.Email);
                    HttpContext.Session.Remove("errorLogin");
                    return RedirectToPage("./Managers/Index");
                }

                if (user.RoleId == 1)
                {
                    HttpContext.Session.SetComplexData("user", user);
                    HttpContext.Session.SetString("userName", user.Email);
                    HttpContext.Session.SetString("useradmin", user.FirstName + " " + user.LastName);
                    HttpContext.Session.Remove("errorLogin");
                    return RedirectToPage("./Customers/HomePage");
                }
            }
            else
            {
                HttpContext.Session.SetString("errorLogin", "Login Error");
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            return null;
        }
    }

}
