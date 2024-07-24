using DTO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.SessionExtensions;

namespace Web.Pages.Shared
{
    public class _HeaderModel : PageModel
    {
        private readonly IHttpContextAccessor _contextAccessor; 
        public _HeaderModel(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        [BindProperty]
        public User userToget { get; set; }
        public void OnGet()
        {
            userToget = HttpContext.Session.GetComplexData<User>("user");
            ViewData["UserName"] = userToget.FirstName +" "+userToget.LastName;
            ViewData["UserId"] =userToget.UserId;
        }
    }
}
