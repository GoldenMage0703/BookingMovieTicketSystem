using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;
using System;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IRepository<User> repository;
        private readonly PRN221_ProjectContext projectContext;

        public RegisterModel(IRepository<User> repository, PRN221_ProjectContext projectContext)
        {
            this.repository = repository;
            this.projectContext = projectContext;
        }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string UserName { get; set; } // Email
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string Phone { get; set; }
        [BindProperty]
        public string Address { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Password != ConfirmPassword)
            {
               
                return Page();
            }

            // Check if the user already exists
            var existingUser = projectContext.Users.FirstOrDefault(x => x.Email.Equals(UserName));
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists.");
                HttpContext.Session.SetString("errorSignup", "Signup Error");
                ErrorMessage = "Gmail already using.";
                return Page();
            }

            // Create a new user
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = UserName,
                Password = Password, // Ideally, hash the password before saving
                Phone = Phone,
                Address = Address,
                RegistrationDate = DateTime.Now,
                RoleId = 1 // Default role; adjust as needed
            };

            await repository.CreateItem(user);
            

            // Redirect to login page or any other page
            return RedirectToPage("/Login");
        }
    }
}
