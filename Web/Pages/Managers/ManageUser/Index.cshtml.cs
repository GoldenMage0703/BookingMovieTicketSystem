using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DTO.Models;

namespace Web.Pages.Managers.ManageUser
{
    public class IndexModel : PageModel
    {
        private readonly DTO.Models.PRN221_ProjectContext _context;

        public IndexModel(DTO.Models.PRN221_ProjectContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Role).ToListAsync();
            }
        }
    }
}
